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
            var languageBases = new List<Type>();
            languageBases.Add(
                typeof(AntMe.Deutsch.Basisameise));
            languageBases.Add(
                typeof(AntMe.English.BaseAnt));

            List<PlayerInfo> result = new List<PlayerInfo>();

            try
            {
                Assembly assembly = Assembly.LoadFile(filename);
                Type[] types;

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
                    if (languageBases.Contains(t.BaseType))
                    {
                        try
                        {
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
