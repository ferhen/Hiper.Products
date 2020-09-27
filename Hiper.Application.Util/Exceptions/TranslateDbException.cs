using System;

namespace Hiper.Application.Util.Exceptions
{
    public static class TranslateDbException
    {
        private const string DuplicateKey = "Cannot insert duplicate key row in object";
        private const string ForeignKey = "The INSERT statement conflicted with the FOREIGN KEY constraint";
        private const string DeleteConstraint = "The DELETE statement conflicted with the REFERENCE constraint";

        public static string TranslateMessageSQLServer(this Exception exception)
        {
            var message = SourceExceptionMessage(exception);

            if (message.StartsWith(DuplicateKey))
                return "O sistema não pode inserir os dados, pois já existe uma entidade com a mesma identificação";
            if (message.StartsWith(ForeignKey))
                return "Ocorreu um erro relacionado a chave estrangeira, favor contate o administrador.";
            if (message.StartsWith(DeleteConstraint))
                return "Não é possível fazer a exclusão, pois há uma outra entidade relacionada. Exclua estes dados antes de excluir esta entidade.";

            return message;
        }

        private static string SourceExceptionMessage(Exception ex)
        {
            var message = ex.Message;
            while (ex.InnerException != null)
            {
                ex = ex.InnerException;
                message = ex.Message;
            }
            return message;
        }
    }
}
