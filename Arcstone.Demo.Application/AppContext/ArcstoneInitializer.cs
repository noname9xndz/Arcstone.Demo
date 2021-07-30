using Arcstone.Demo.Application.Models.Entities;
using Microsoft.Extensions.DependencyInjection;
using Noname.UnitOfWork.Lib.Initializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Arcstone.Demo.Application.AppContext
{
    public class ArcstoneInitializer : DbInitializer<ArcstoneContext>
    {
        public ArcstoneInitializer(IServiceScopeFactory scopeFactory) : base(scopeFactory)
        {
        }

        public override async Task SeedData()
        {
            var scopeFactory = GetServiceScopeFactory();
            if (scopeFactory != null)
            {
                using (var serviceScope = scopeFactory.CreateScope())
                {
                    using (var context = serviceScope.ServiceProvider.GetService<ArcstoneContext>())
                    {
                        SeedProjects(context);
                        await context.SaveChangesAsync();
                    }
                }
            }

            await Task.CompletedTask;
        }

        private void SeedProjects(ArcstoneContext context)
        {
            if (!context.Projects.Any())
            {
                context.Projects.AddRange(new ProjectModel()
                {
                    Name = "Project 1",
                    Description = "Project s1",
                    Id = Guid.Parse("514a1c5e-f87a-47b1-9043-54beda1ccb76"),
                    CreatedDate = 1627400874,
                    ModifiedDate = 1627400874,
                    ClientName = "MILO",
                    CreatedBy = "3ea5bcb0-7f99-432f-8fca-d94fea455e62",
                    ModifiedBy = "3ea5bcb0-7f99-432f-8fca-d94fea455e62",
                    Active = true,
                    Tasks = new List<TaskModel>()
                    {
                        new TaskModel()
                        {
                            Name = "Task1-Project1",
                            Active = true,
                            Description = "abc",
                            CreatedBy = "a5a8e5d2-b6c2-49d9-a4d9-281a2e0d03eb",
                            ModifiedBy = "a5a8e5d2-b6c2-49d9-a4d9-281a2e0d03eb",
                            CreatedDate = 1627658750,
                            ModifiedDate = 1627658750,
                            StartTime = 1627658750,
                            Date = 1627658000,
                            EndTime = 1627662350,
                            TotalTime = 1
                        },
                        new TaskModel()
                        {
                            Name = "Task2-Project1",
                            Active = true,
                            Description = "abc",
                            CreatedBy = "a5a8e5d2-b6c2-49d9-a4d9-281a2e0d03eb",
                            ModifiedBy = "a5a8e5d2-b6c2-49d9-a4d9-281a2e0d03eb",
                            CreatedDate = 1627561805,
                            Date = 1627491600,
                            ModifiedDate = 1627561805,
                            StartTime = 1627561805,
                            EndTime = 1627576205,
                            TotalTime = 4
                        },
                        new TaskModel()
                        {
                            Name = "Task3-Project1",
                            Active = true,
                            Description = "abc",
                            CreatedBy = "3ea5bcb0-7f99-432f-8fca-d94fea455e62",
                            ModifiedBy = "3ea5bcb0-7f99-432f-8fca-d94fea455e62",
                            CreatedDate = 1627565557,
                            Date = 1627491600,
                            ModifiedDate = 1627565557,
                            StartTime = 1627565557,
                            EndTime = 1627576357,
                            TotalTime = 3
                        },
                        new TaskModel()
                        {
                            Name = "Task4-Project1",
                            Active = true,
                            Description = "abc",
                            CreatedBy = "3ea5bcb0-7f99-432f-8fca-d94fea455e62",
                            ModifiedBy = "3ea5bcb0-7f99-432f-8fca-d94fea455e62",
                            CreatedDate = 1627572757,
                            ModifiedDate = 1627572757,
                            Date = 1627491600,
                            StartTime = 1627572757,
                            EndTime = 1627576357,
                            TotalTime = 1
                        },
                        new TaskModel()
                        {
                            Name = "Task5-Project1",
                            Active = true,
                            Description = "abc",
                            CreatedBy = "3ea5bcb0-7f99-432f-8fca-d94fea455e62",
                            ModifiedBy = "3ea5bcb0-7f99-432f-8fca-d94fea455e62",
                            CreatedDate = 1627641362,
                            ModifiedDate = 1627641362,
                            Date = 1627578000,
                            StartTime = 1627641362,
                            EndTime = 1627659362,
                            TotalTime = 5
                        },
                        new TaskModel()
                        {
                            Name = "Task6-Project1",
                            Active = true,
                            Description = "abc",
                            CreatedBy = "3ea5bcb0-7f99-432f-8fca-d94fea455e62",
                            ModifiedBy = "3ea5bcb0-7f99-432f-8fca-d94fea455e62",
                            CreatedDate = 1627634387,
                            ModifiedDate = 1627634387,
                            Date = 1627578000,
                            StartTime = 1627634387,
                            EndTime = 1627659587,
                            TotalTime = 7
                        }
                    }
                },
                    new ProjectModel()
                    {
                        Name = "Project 2",
                        Description = "Project s2",
                        Id = Guid.Parse("2da2ea77-b217-4b17-8831-f80ba35d8bd7"),
                        CreatedDate = 1627400874,
                        ModifiedDate = 1627400874,
                        ClientName = "TIZEN",
                        CreatedBy = "3ea5bcb0-7f99-432f-8fca-d94fea455e62",
                        ModifiedBy = "3ea5bcb0-7f99-432f-8fca-d94fea455e62",
                        Active = true,
                        Tasks = new List<TaskModel>()
                        {
                            new TaskModel()
                            {
                                Name = "Task1-Project2",
                                Active = true,
                                Description = "abc",
                                CreatedBy = "3ea5bcb0-7f99-432f-8fca-d94fea455e62",
                                ModifiedBy = "3ea5bcb0-7f99-432f-8fca-d94fea455e62",
                                CreatedDate = 1627641362,
                                ModifiedDate = 1627641362,
                                Date = 1627578000,
                                StartTime = 1627641362,
                                EndTime = 1627659362,
                                TotalTime = 5
                            },
                            new TaskModel()
                            {
                            Name = "Task2-Project2",
                            Active = true,
                            Description = "abc",
                            CreatedBy = "3ea5bcb0-7f99-432f-8fca-d94fea455e62",
                            ModifiedBy = "3ea5bcb0-7f99-432f-8fca-d94fea455e62",
                            CreatedDate = 1627634387,
                            ModifiedDate = 1627634387,
                            Date = 1627578000,
                            StartTime = 1627634387,
                            EndTime = 1627659587,
                            TotalTime = 7
                        },
                            new TaskModel()
                            {
                                Name = "Task3-Project2",
                                Active = true,
                                Description = "abc",
                                CreatedBy = "a5a8e5d2-b6c2-49d9-a4d9-281a2e0d03eb",
                                ModifiedBy = "a5a8e5d2-b6c2-49d9-a4d9-281a2e0d03eb",
                                CreatedDate = 1627648742,
                                ModifiedDate = 1627648742,
                                Date = 1627578000,
                                StartTime = 1627648742,
                                EndTime = 1627659542,
                                TotalTime = 3
                            },
                            new TaskModel()
                            {
                                Name = "Task3-Project2",
                                Active = true,
                                Description = "abc",
                                CreatedBy = "a5a8e5d2-b6c2-49d9-a4d9-281a2e0d03eb",
                                ModifiedBy = "a5a8e5d2-b6c2-49d9-a4d9-281a2e0d03eb",
                                CreatedDate = 1627648742,
                                ModifiedDate = 1627648742,
                                Date = 1627578000,
                                StartTime = 1627648742,
                                EndTime = 1627659542,
                                TotalTime = 3
                            },

                            new TaskModel()
                            {
                            Name = "Task4-Project2",
                            Active = true,
                            Description = "abc",
                            CreatedBy = "a5a8e5d2-b6c2-49d9-a4d9-281a2e0d03eb",
                            ModifiedBy = "a5a8e5d2-b6c2-49d9-a4d9-281a2e0d03eb",
                            CreatedDate = 1627672514,
                            ModifiedDate = 1627672514,
                            Date = 1627664400,
                            StartTime = 1627672514,
                            EndTime = 1627676212,
                            TotalTime = 3
                        }
                        }
                    });
            }
        }
    }
}