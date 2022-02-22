using Azure;
using System;
using Azure.AI.TextAnalytics;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Example
{
    static class Program
    {
        private static readonly AzureKeyCredential credentials = new AzureKeyCredential("");
        private static readonly Uri endpoint = new Uri("");
        
        // Example method for extracting information from healthcare-related text 
        static async Task HealthExample(TextAnalyticsClient client, string document)
        {
            List<string> batchInput = new List<string>()
            {
                document
            };
            AnalyzeHealthcareEntitiesOperation healthOperation = await client.StartAnalyzeHealthcareEntitiesAsync(batchInput);
            await healthOperation.WaitForCompletionAsync();

            await foreach (AnalyzeHealthcareEntitiesResultCollection documentsInPage in healthOperation.Value)
            {
                Console.WriteLine($"Results of Azure Text Analytics for health async model, version: \"{documentsInPage.ModelVersion}\"");
                Console.WriteLine("");

                foreach (AnalyzeHealthcareEntitiesResult entitiesInDoc in documentsInPage)
                {
                    if (!entitiesInDoc.HasError)
                    {
                        foreach (var entity in entitiesInDoc.Entities)
                        {
                            // view recognized healthcare entities
                            Console.WriteLine($"  Entity: {entity.Text}");
                            Console.WriteLine($"  Category: {entity.Category}");
                            Console.WriteLine($"  Offset: {entity.Offset}");
                            Console.WriteLine($"  Length: {entity.Length}");
                            Console.WriteLine($"  NormalizedText: {entity.NormalizedText}");
                            Console.WriteLine($"  Confidence: {entity.ConfidenceScore}");

                            Console.WriteLine($"  Snomed Code:");

                            

                            foreach (var dataSource in entity.DataSources.Where(d => d.Name == "SNOMEDCT_US"))
                            {
                                Console.WriteLine($"     Name: {dataSource.Name}");
                                Console.WriteLine($"     Id: {dataSource.EntityId}");
                                Console.WriteLine();
                            }

                        }
                        Console.WriteLine($"  Found {entitiesInDoc.EntityRelations.Count} relations in the current document:");
                        Console.WriteLine("");

                        // view recognized healthcare relations
                        foreach (HealthcareEntityRelation relations in entitiesInDoc.EntityRelations)
                        {
                            Console.WriteLine($"    Relation: {relations.RelationType}");
                            Console.WriteLine($"    For this relation there are {relations.Roles.Count} roles");

                            // view relation roles
                            foreach (HealthcareEntityRelationRole role in relations.Roles)
                            {
                                Console.WriteLine($"      Role Name: {role.Name}");

                                Console.WriteLine($"      Associated Entity Text: {role.Entity.Text}");
                                Console.WriteLine($"      Associated Entity Category: {role.Entity.Category}");
                                Console.WriteLine("");
                            }
                            Console.WriteLine("");
                        }
                    }
                    else
                    {
                        Console.WriteLine("  Error!");
                        Console.WriteLine($"  Document error code: {entitiesInDoc.Error.ErrorCode}.");
                        Console.WriteLine($"  Message: {entitiesInDoc.Error.Message}");
                    }
                    Console.WriteLine("");
                }
            }
        }

        static async Task Main(string[] args)
        {
            var client = new TextAnalyticsClient(endpoint, credentials);

            Console.WriteLine("Enter some text to analyze (or 'exit' to exit):");

            while (true)
            {
                Console.Write("> ");
                var text = Console.ReadLine();
                if (text.ToLowerInvariant() == "exit")
                {
                    break;
                }
                await HealthExample(client, text);
            }
        }
    }
}