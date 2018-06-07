using System;
using System.Collections.Generic;
using System.Linq;

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

                var st = ctx.Students.Where(s => s.StudentId == 1).First();
                var dz = ctx.Dozents.Where(d => d.DozentId == 1).First();

                // Beziehung hinzufügen
                var bez = new Bekanntschaft() {
                    Student = st,
                    Dozent = dz
                };

                if(st.Bekanntschaften == null) {
                    st.Bekanntschaften = new List<Bekanntschaft>();
                }

                if(dz.Bekanntschaften == null) {
                    dz.Bekanntschaften = new List<Bekanntschaft>();
                } 

                st.Bekanntschaften.Add(bez);
                dz.Bekanntschaften.Add(bez);
                ctx.SaveChanges();
            }
        }
    }
}
