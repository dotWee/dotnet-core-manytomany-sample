using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ManyToMany
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Beispielanwendung für Many-to-Many mit Dotnet Core und EF-Core");

            using(var ctx = new ManyToManyContext()) {
                // Falls nötig, Teststudenten anlegen
                for(int i = 1; i < 100; i++) {
                    if(ctx.Students.Where(s => s.StudentId == i).Count() == 0) {
                        var s = new Student() {
                            Matrikelnummer = $"12345678{i}",
                            Vorname = "Max",
                            Nachname = "Mustermann"
                        };
                        ctx.Students.Add(s);
                        ctx.SaveChanges();
                    }
                }

                // Falls nötig, Testdozent anlegen
                if(ctx.Dozents.Where(d => d.DozentId == 1).Count() == 0) {
                    var d = new Dozent() {
                        Vorname = "Moritz",
                        Nachname = "Musterlehrer"
                    };
                    ctx.Dozents.Add(d);
                    ctx.SaveChanges();
                }

                var st = ctx.Students.Where(s => s.StudentId == 11)
                                     .Include(s => s.Bekanntschaften)
                                     //.ThenInclude(b => b.Dozent)
                                     .First();
                var dz = ctx.Dozents.Where(d => d.DozentId == 1).Include(s => s.Bekanntschaften).First();

                // Beziehung hinzufügen
                var bek = new Bekanntschaft() {
                    Student = st,
                    Dozent = dz
                    //StudentId = st.StudentId,
                    //DozentId = dz.DozentId
                };

                ctx.Entry(st).State = EntityState.Modified;
                
                st.Bekanntschaften.Add(bek);
                ctx.SaveChanges();
            }
        }
    }
}
