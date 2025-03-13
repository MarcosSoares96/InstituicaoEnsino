using InstituicaoEnsino.Models;

namespace InstituicaoEnsino.Data
{
    public class IESDbInitializer
    {
        public static void Initialize(IESContext context)
        {
            context.Database.EnsureCreated();

            if (context.Departamentos.Any())
            {
                return;
            }

            var departamentos = new Departamento[]
            {
                new Departamento {Nome = "Ciência da Computação"},
                new Departamento {Nome = "Ciência de Alimentos"}
            };

            foreach (Departamento d in departamentos)
            {
                context.Departamentos.Add(d);
            }

            context.SaveChanges();
        }
    }

    /*Obs:
     Método EnsureCreated:
         Resumo:
             Ensures that the database for the context exists.    
         Comentários:
             • If the database exists and has any tables, then no action is taken. Nothing
             is done to ensure the database schema is compatible with the Entity Framework
             model.
             • If the database exists but does not have any tables, then the Entity Framework
             model is used to create the database schema.
             • If the database does not exist, then the database is created and the Entity
             Framework model is used to create the database schema.   
    */
}
