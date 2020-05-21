﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.IO;

namespace SportsStore.Models {
    public static class SeedData {
        public static void EnsurePopulated(IApplicationBuilder app) {
            ApplicationDbContext context = app.ApplicationServices.GetRequiredService<ApplicationDbContext>();
            context.Database.Migrate();
            EnsureProducts(context);
            EnsureViews(context);
        }

        private static void EnsureViews(ApplicationDbContext context) {
            if (!context.Views.Any()) {
                context.Views.AddRange(
                    new RazerView() {
                        Location = "/NotSpecified.cshtml",
                        LastRequested = null,
                        LastModified = DateTime.Now,
                        HTMLContent = @"<h1>Oops.</h1><p>Looks like you forgot to specify which view we should show you.</p>"
                    },
                    new RazerView() {
                        Location = "/Editor.cshtml",
                        LastRequested = null,
                        LastModified = DateTime.Now
                    },
                    new RazerView() {
                        Location = "/EditorBottomResult.cshtml",
                        LastRequested = null,
                        LastModified = DateTime.Now,
                        CSSContent = "<style>textarea{width: 100%; min-height: 100px; border: 1px solid #CCC; box-sizing: border-box; padding: 0 0 0 30px; resize: vertical; line-height: 16px; font-size: 13px; color: black;}iframe{bottom: 0; width: 100%; height: 100%; border: black; background: black; position: relative;}body{height: 100%;}#editorRows{margin: 0;}#editorRows > div{padding: 0; height: 100%;}.codeHeader{margin: 0; padding-left: 20px; background-color: black;}.right-border{border-right: 1px solid gray;}.CodeMirror{height: auto;}#Result .codeHeader{background-color: none; margin-top: 20px; padding: 0;}</style>",
                        HTMLContent = @"<head> <title>Code Editor</title> <meta name='viewport' content='width=device-width, initial-scale=1'/> <link rel='stylesheet' asp-href-include='/lib/codemirror/codemirror.css'/> <link href='~/lib/codemirror/theme/3024-night.css' rel='stylesheet'/></head><div class='container'> <form id='viewForm' asp-action='SaveView' method='post'> <textarea class='d-none' asp-for='Content'> </textarea> <div class='form-group'> <label asp-for='Location'></label> <input class='form-control' type='text' asp-for='Location'/> </div><div class='row ' id='editorRows'> <div class='right-border col-4' id='HTML'> <h4 class='codeHeader'>HTML</h4> <textarea asp-for='@Model.HTMLContent'></textarea> </div><div class='right-border col-4' id='CSS'> <h4 class='codeHeader'>CSS</h4> <textarea asp-for='@Model.CSSContent'></textarea> </div><div class='right-border col-4' id='JavaScript'> <h4 class='codeHeader'>JavaScript</h4> <textarea asp-for='@Model.JSContent'></textarea> </div></div><button type='submit'>Save</button> </form> <div class='col-12' id='Result'> <h4 class='codeHeader'>Result</h4> <iframe id='code' ></iframe> </div></div>",
                        JSContent = "< script src =\"~/lib/codemirror/codemirror.js\"></script>< script src =\"~/lib/codemirror/mode/css/css.min.js\"></script>< script src =\"~/lib/codemirror/mode/xml/xml.min.js\"></script>< script src =\"~/lib/codemirror/addon/edit/closetag.min.js\"></script>< script src =\"~/lib/codemirror/addon/edit/matchbrackets.min.js\"></script>< script src =\"~/lib/codemirror/addon/edit/closebrackets.min.js\"></script>< script src =\"~/lib/codemirror/mode/javascript/javascript.min.js\"></script>< script src =\"~/lib/codemirror/addon/selection/active-line.min.js\"></script>< script src =\"~/lib/codemirror/addon/lint/javascript-lint.min.js\"></script>< script src =\"~/lib/codemirror/addon/lint/css-lint.min.js\"></script>< script src =\"~/lib/codemirror/addon/lint/html-lint.min.js\"></script>< script > var editorHTML = CodeMirror.fromTextArea(document.getElementById(\"HTMLContent\"), { lint: true, htmlMode: true, mode: \"text/html\", lineNumbers: true, theme: \"3024-night\", autoCloseTags: true, matchBrackets: true, viewportMargin: 30, styleActiveLine: true, autoCloseBrackets: true, value: \"<h1> Welcome </h1>\", gutters: [\"CodeMirror-lint-markers\"] }); var editorCSS = CodeMirror.fromTextArea(document.getElementById(\"CSSContent\"), { lint: true, mode: \"text/css\", lineNumbers: true, matchBrackets: true, theme: \"3024-night\", styleActiveLine: true, autoCloseBrackets: true, gutters: [\"CodeMirror-lint-markers\"] }); var editorJavaScript = CodeMirror.fromTextArea(document.getElementById(\"JSContent\"), { lint: true, mode: \"javascript\", lineNumbers: true, matchBrackets: true, theme: \"3024-night\", styleActiveLine: true, autoCloseBrackets: true, gutters: [\"CodeMirror-lint-markers\"] }); editorCSS.refresh(); editorHTML.refresh(); editorJavaScript.refresh(); // */ /* editorHTML.on(\"keyup\", documentChanges); editorCSS.on(\"change\", documentChanges); editorJavaScript.on(\"change\", documentChanges); // */ $(document).ready(function (){$(\"#code\").on(\"load\", function (){var head=$(\"#code\").contents().find(\"head\"); var bootstrapcss='<link rel=\"stylesheet\" href=\"/lib/twitter-bootstrap/css/bootstrap_dark.min.css\">'; var fontawesome='<link rel=\"stylesheet\" href=\"/lib/font-awesome/css/font-awesome.min.css\">'; var jquery='<script src=\"/lib/jquery/dist/jquery.min.js\"></script>'; var bootstrapjs='< script src =\"/lib/twitter-bootstrap/js/bootstrap.min.js\"></script>'; $(\"#code\").contents().find(\"head\").append(bootstrapcss); $(\"#code\").contents().find(\"head\").append(fontawesome); $(\"#code\").contents().find(\"head\").append(jquery); $(\"#code\").contents().find(\"head\").append(bootstrapjs); /* head.append(bootstrapcss); head.append(fontawesome); head.append(jquery); head.append(bootstrapjs); //$(head).append($(\"< link />\",{rel: \"stylesheet\", href: \"/lib/font-awesome/css/*.css\" , type: \"text/css\"})); $(head).append($(\"< script />\",{src: \"~/lib/jquery/dist/jquery.min.js\"})); // */});}); window.setInterval(function (){documentChanges();}, 5000); $(\"#viewForm\").submit(documentChanges); function documentChanges(){var html=editorHTML.getValue(); var css=editorCSS.getValue(); var js=editorJavaScript.getValue(); $(\"#Content\").val(\"< style >    \" + css + \"</ style >\" + html + \"< script >\" + js + \"</script>\"); var code=document.getElementById(\"code\").contentWindow.document; code.open(); //code.writeln(html); if (html) code.writeln(html + \"< style >    \" + css + \"</ style >\" + \"< script >\" + js + \"</script>\"); code.close();};<"
                        
                    }
                );
                context.SaveChanges();
            }

        }

        private static void EnsureProducts(ApplicationDbContext context) {
            if (!context.Products.Any()) {
                context.Products.AddRange(
                new Product {
                    Name = "Kayak", Description = "A boat for one person",
                    Category = "Watersports", Price = 275
                },
                new Product {
                    Name = "Lifejacket",
                    Description = "Protective and fashionable",
                    Category = "Watersports", Price = 48.95m
                },
                new Product {
                    Name = "Soccer Ball",
                    Description = "FIFA-approved size and weight",
                    Category = "Soccer", Price = 19.50m
                },
                new Product {
                    Name = "Corner Flags",
                    Description = "Give your playing field a professional touch",
                    Category = "Soccer", Price = 34.95m
                },
                new Product {
                    Name = "Stadium",
                    Description = "Flat-packed 35,000-seat stadium",
                    Category = "Soccer", Price = 79500
                },
                new Product {
                    Name = "Thinking Cap",
                    Description = "Improve brain efficiency by 75%",
                    Category = "Chess", Price = 16
                },
                new Product {
                    Name = "Unsteady Chair",
                    Description = "Secretly give your opponent a disadvantage",
                    Category = "Chess", Price = 29.95m
                },
                new Product {
                    Name = "Human Chess Board",
                    Description = "A fun game for the family",
                    Category = "Chess", Price = 75
                },
                new Product {
                    Name = "Bling-Bling King",
                    Description = "Gold-plated, diamond-studded King",
                    Category = "Chess", Price = 1200
                }
                );
                context.SaveChanges();
            }

        }

    }
}
