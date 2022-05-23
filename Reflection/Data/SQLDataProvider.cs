﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Data;
using Newtonsoft.Json;
using System.Data.Common;
using System.Data.SqlClient;

namespace Reflection {
    /// <summary>
    /// Class for data manipulation
    /// </summary>
    public abstract class SqlDataProvider : DataProvider {

        public SqlDataProvider(string connectionString)
            : base(connectionString) {
        }

        protected override DbCommand GetDbCommand(String command, DbConnection connection) {
            return new SqlCommand(command, (SqlConnection)connection);
        }

        protected override DbConnection GetDbConnection() {
            return new SqlConnection(this.ConnectionString);
        }

        protected override DbDataAdapter GetDbDataAdapter(String statement, DbConnection transactionConnection) {
            SqlDataAdapter adapter;
            if (transactionConnection != null) {
                adapter = new SqlDataAdapter(statement, (SqlConnection)transactionConnection);
            } else {
                adapter = new SqlDataAdapter(statement, new SqlConnection(this.ConnectionString));
            }

            return adapter;
        }

        protected override Dictionary<string, object> ProcessParameter(object parameterObject, ref DbParameterCollection parameters) {
            Dictionary<string, object> parameterDictionary = null;

            // parameters
            if (parameterObject != null && parameterObject is Dictionary<string, object>) {
                parameterDictionary = parameterObject as Dictionary<string, object>;
            } else if (parameterObject != null) {
                if (parameterObject.GetType().IsArray)
                    throw new ArgumentException("parameterObject must be an object with key/value pairs, or a Dictionary(string, object).");

                parameterDictionary = _createSqlParameterList(parameterObject);
            }

            // parameters
            if (parameterDictionary != null)
                foreach (var p in parameterDictionary) {
                    //only allowed table types 
                    if (p.Value is List<string> || p.Value is List<Guid> || p.Value is List<int>) {
                        DataTable dt = new DataTable();
                        dt.Columns.Add("Item");
                        foreach (var pp in (ICollection)p.Value)
                            dt.Rows.Add(pp);
                        parameters.Add(new SqlParameter(p.Key, dt));

                    } else
                        parameters.Add(new SqlParameter(p.Key, p.Value));

                }

            return parameterDictionary;
        }

        /// <summary>
        /// Creates a list of SqlParameters from an object
        /// </summary>
        /// <param name="parameters"></param>
        /// <remarks>Complex properties are XmlSerialized</remarks>
        /// <returns></returns>
        private static Dictionary<string, object> _createSqlParameterList(object parameters) {
            var result = new Dictionary<string, object>();
            if (parameters != null) {
                // add properties
                foreach (var prop in parameters.GetType().GetProperties()) {
                    var attr = (SqlQueryParameterAttribute)prop.GetCustomAttributes(typeof(SqlQueryParameterAttribute), false).FirstOrDefault();

                    // short-circuit on ignore prop
                    if (attr != null && (attr.Ignore || attr.IgnoreInOnly))
                        continue;

                    string name = prop.Name;
                    object value = prop.GetValue(parameters, null);

                    if (attr != null) {
                        // check attrs
                        if (String.IsNullOrEmpty(attr.ParameterName) == false) {
                            name = attr.ParameterName;
                        }
                        if (attr.MaxLength > 0 && value is String) {
                            value = value.ToString().Substring(0, attr.MaxLength);
                        }
                        else if (attr.Serialize == SerializationType.Json) {
                            value = JsonConvert.SerializeObject(value);
                        }
                    }

                    // throw error on collections or arrays UNLESS they're byte[] or List<string> or List<Guid> or List<int>
                    if (value is byte[]) { /* allow */ } else if (value is List<string> || value is List<Guid> || value is List<int>) { /* allow */ } else if (value is ICollection || (value != null && value.GetType().IsArray))
                        throw new ArgumentException("Parameter cannot be a collection or array: " + name);

                    // skip if nullable and null				
                    if (value == null
                        && prop.PropertyType.IsGenericType
                        && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                        continue;

                    if (value != null && value.GetType().IsEnum)
                        value = Convert.ToInt64(value);

                    // add to dict
                    result.Add(name, value);
                }
            }

            return result;
        }

    }

}
