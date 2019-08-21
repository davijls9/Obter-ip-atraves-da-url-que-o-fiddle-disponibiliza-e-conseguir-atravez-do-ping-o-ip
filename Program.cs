using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;

namespace Script_Leitura_de_IP
{
    class Texto { public string texto; }

    class Program
    {
        //Metodo
        public static string ExecutarCMD(string comando)
        {
            using (Process processo = new Process())
            {
                processo.StartInfo.FileName = Environment.GetEnvironmentVariable("comspec");

                // Formata a string para passar como argumento para o cmd.exe
                processo.StartInfo.Arguments = string.Format("/c {0}", comando);

                processo.StartInfo.RedirectStandardOutput = true;
                processo.StartInfo.UseShellExecute = false;
                processo.StartInfo.CreateNoWindow = true;

                processo.Start();
                processo.WaitForExit();

                string saida = processo.StandardOutput.ReadToEnd();
                return saida;
            }
        }

        //Comentario: D:\Download

        //Metodo principal
        static void Main(string[] args)
        {
            int cont = 0; string textoFinal = ""; string executar = "";
            List<Texto> lista = new List<Texto>();

            string[] array = File.ReadAllLines(@"D:\Cursos\Curso Programação orientada a objetos C#\Projetos\source\Script Leitura de IP\Script Leitura de IP\bin\Debug\netcoreapp2.1\Links.txt");

            for (int i = 0; i < array.Length - 1; i++)
            {
                Texto t = new Texto();
                string[] auxiliar = array[i].Split(";");

                //auxiliar[0]

                string[] aux = auxiliar[0].Split(":");
                string[] aux2 = auxiliar[0].Split(":");

                if (aux[0] == "http")
                {
                    t.texto = aux[1].Substring(2, aux[1].Length-2);
                }
                if (aux2[0] == "https")
                {
                    t.texto = aux[1].Substring(2, aux[1].Length - 2);
                }

                lista.Add(t);
            }



            foreach (var item in lista)
            {
                executar = $"ping -n 1 {lista[cont].texto}";
                string[] aux = ExecutarCMD(executar).Split(" ");
                textoFinal += aux[2];
                textoFinal += " \n";
                cont++;
            }

            File.WriteAllText("Arquivo_Com_IPs_Do_Link.txt", textoFinal);
            Console.WriteLine("Clique em alguma tecla.");
            Console.ReadKey();
        }
    }
}
