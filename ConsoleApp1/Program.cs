using System;
using System.IO;

class Program
{
    static void Main()
    {
        string filePath  = "inventos.txt";
        string backupFolder = "Backup";
        string classifiedFolder = "ArchivosClasificados";
        string secretFolder = "ProyectosSecretos";
        string avengersLabFolder = "LaboratorioAvengers";

        // Menú interactivo
        while (true)
        {

            Console.WriteLine("\n...Opciones...:");
            Console.WriteLine("1.\n---CREAR IN ARCHIVO 'INVETOS.TXT'---");
            Console.WriteLine("2.\n---AGG NUEVOS INVENTOS---");
            Console.WriteLine("3.\n---LEER LOS INVENTOS---");
            Console.WriteLine("4.\n---LEER CONTENIDO DEL ARCHIVO---");
            Console.WriteLine("5.\n---COPIARF EL ARCHIVO A 'BACKUP'---");
            Console.WriteLine("6.\n---MOVER UN ARCHIVO A 'ARCHIVOSCLASIFICADOS'---");
            Console.WriteLine("7.\n---CREAR UJNA CARPETA 'PROYECTOSSECRETOS'---");
            Console.WriteLine("8.\n---LISTAR ARCHIVOS EN 'LABORATORIOAVENGUERS'---");
            Console.WriteLine("9.\n---ELIMINAR ARCHIVO 'INVETOS.TXT'---");
            Console.WriteLine("10.\n___SALIR___");
            Console.Write("\n___SELECCIONA ALGUNA OPCION ");
            string option = Console.ReadLine();

            try
            {
                switch (option)
                {
                    case "1":
                        CrearArchivo(filePath);
                        break;
                    case "2":
                        AgregarInventos(filePath);
                        break;
                    case "3":
                        LeerLineaPorLinea(filePath);
                        break;
                    case "4":
                        LeerTodoElTexto(filePath);
                        break;
                    case "5":
                        CopiarArchivo(filePath, Path.Combine(backupFolder, "inventos.txt"));
                        break;
                    case "6":
                        MoverArchivo(filePath, Path.Combine(classifiedFolder, "inventos.txt"));
                        break;
                    case "7":
                        CrearCarpeta(secretFolder);
                        break;
                    case "8":
                        ListarArchivos(avengersLabFolder);
                        break;
                    case "9":
                        EliminarArchivo(filePath, backupFolder);
                        break;
                    case "10":
                        return;
                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }

    // Función para que se cree un archivo
    //funciones if-else
    static void CrearArchivo(string filePath)
    {
        if (!File.Exists(filePath))
        {
            using (StreamWriter writer = File.CreateText(filePath))
            {
                Console.WriteLine($"Archivo '{filePath}' creado.");
            }
        }
        else
        {
            Console.WriteLine($"El archivo '{filePath}' ya existe.");
        }
    }

    // Función para agregar inventos a la carpeta 
    static void AgregarInventos(string filePath)
    {
        VerificarArchivoExistente(filePath);
        int counter = ObtenerSiguienteNumero(filePath);
        Console.WriteLine("\n__Escribe los nombres de los nuevos inventos__");
        Console.WriteLine("\n__uno por línea).Escribe 'fin' para terminar__");
     
        using (StreamWriter writer = new StreamWriter(filePath, append: true))
        {
            string input;
            while ((input = Console.ReadLine()) != "fin")
            {
                writer.WriteLine($"{counter}. {input}");
                counter++;
            }
        }

        Console.WriteLine("\n__Los inventos han sido aguardados__");
    }

    // Función para leer línea por línea
    static void LeerLineaPorLinea(string filePath)
    {
        VerificarArchivoExistente(filePath);
        Console.WriteLine("\nInventos guardados:");
        using (StreamReader reader = new StreamReader(filePath))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                Console.WriteLine(line);
            }
        }
    }

    // Función para leer todo el texto del archivo
    static void LeerTodoElTexto(string filePath)
    {
        VerificarArchivoExistente(filePath);
        string content = File.ReadAllText(filePath);
        Console.WriteLine("\nContenido completo del archivo:");
        Console.WriteLine(content);
    }

    // Función para copiar archivo
    static void CopiarArchivo(string origen, string destino)
    {
        VerificarArchivoExistente(origen);
        Directory.CreateDirectory(Path.GetDirectoryName(destino));
        File.Copy(origen, destino, true);
        Console.WriteLine($"Archivo copiado a '{destino}'.");
    }

    // Función para mover archivo
    static void MoverArchivo(string origen, string destino)
    {
        VerificarArchivoExistente(origen);
        Directory.CreateDirectory(Path.GetDirectoryName(destino));
        File.Move(origen, destino);
        Console.WriteLine($"Archivo movido a '{destino}'.");
    }

    // Función para crear una carpeta
    static void CrearCarpeta(string nombreCarpeta)
    {
        Directory.CreateDirectory(nombreCarpeta);
        Console.WriteLine($"Carpeta '{nombreCarpeta}' creada.");
    }

    // Función para eliminar archivo
    static void EliminarArchivo(string filePath, string backupFolder)
    {
        VerificarArchivoExistente(filePath);
        CopiarArchivo(filePath, Path.Combine(backupFolder, "inventos.txt"));
        File.Delete(filePath);
        Console.WriteLine($"Archivo '{filePath}' eliminado después de hacer una copia de seguridad.");
    }

    // Función para listar archivos en una carpeta
    static void ListarArchivos(string ruta)
    {
        if (Directory.Exists(ruta))
        {
            Console.WriteLine($"\nArchivos en '{ruta}':");
            string[] files = Directory.GetFiles(ruta);
            foreach (var file in files)
            {
                Console.WriteLine(Path.GetFileName(file));
            }
        }
        else
        {
            Console.WriteLine($"La carpeta '{ruta}' no existe.");
        }
    }

    // Función auxiliar para obtener el siguiente número de invento
    static int ObtenerSiguienteNumero(string filePath)
    {
        int counter = 1;

        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);
            if (lines.Length > 0)
            {
                string lastLine = lines[lines.Length - 1];
                string[] parts = lastLine.Split('.');
                if (int.TryParse(parts[0], out int lastNumber))
                {
                    counter = lastNumber + 1; ;
                }
            }
        }

        return counter;
    }

    // Función  extra para saber si el archivo que buscamos existe 
    static void VerificarArchivoExistente(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"Este archivo '{filePath}' no existe. ¡Ultron debe haberlo borrado! ¡Busca el archivo y elimina a Ultron!");
            Console.WriteLine("Mucha suerte ayudando al senor stark ");
        }
    }
}


//    |  -     -  |
//    |     |     | 
//    |    ~~~    |


//          {

//        ULTRON 