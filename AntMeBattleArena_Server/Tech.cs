using AntMe.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AntMeBattleArena_Server
{
    public class Tech
    {
        public static List<PlayerInfo> GetAntsFromAssembly(string filename)
        {
            // Klassen, die Spielerinfos enthalten
            var languageBases = new List<Type>();
            languageBases.Add(
                typeof(AntMe.Deutsch.Basisameise));
            languageBases.Add(
                typeof(AntMe.English.BaseAnt));

            List<PlayerInfo> result = new List<PlayerInfo>();

            try
            {
                // DLL laden
                Assembly assembly = Assembly.LoadFile(filename);
                Type[] types;

                // Enthaltene Klassen möglichst fehlerfrei auslesen
                // Stets unsicher, da die DLL zur Laufzeit analysiert wird
                try
                {
                    types = assembly.GetTypes();
                }
                catch (ReflectionTypeLoadException ex)
                {
                    types = ex.Types.Where(x => x != null).ToArray();
                }

                foreach (Type t in types)
                {
                    // Alle Klassen auf mögliche Spieler prüfen
                    if (languageBases.Contains(t.BaseType))
                    {
                        try
                        {
                            // Simulations-DLL liest Attribute aus
                            result.Add(AiAnalysis.FindPlayerInformation(filename, t.FullName));
                        }
                        catch { }
                    }
                }
            }
            catch (Exception e)
            {

            }
            
            return result;
        }
    }
}
