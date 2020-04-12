using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;


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
                        Content = @"<h1>Oops.</h1><p>Looks like you forgot to specify which view we should show you.</p>"
                    },
                    new RazerView() {
                        Location = "/Editor.cshtml",
                        LastRequested = null,
                        LastModified = DateTime.Now,
                        Content = @"<style>body{text-align: center;}textarea{width: 32%; float: top; min-height: 250px; overflow: scroll; margin: auto; display: inline-block; background: #f4f4f9; outline: none; font-family: Courier, sans-serif; font-size: 14px;}iframe{bottom: 0; position: relative; width: 100%; height: 35em;}</style><html><head> <title>Code Editor</title> <meta name='viewport' content='width=device-width, initial-scale=1'/> <link rel='stylesheet' href='style.css'/></head><body> <textarea id='html' placeholder='HTML'></textarea> <textarea id='css' placeholder='CSS'></textarea> <textarea id='js' placeholder='JavaScript'></textarea> <iframe id='code'></iframe>"
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
