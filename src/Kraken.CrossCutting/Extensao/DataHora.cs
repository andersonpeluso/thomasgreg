namespace Kraken.CrossCutting.Extensao
{
    public static class DataHora
    {
        public static DateTime ObterHorarioBrasilia()
        {
            var dateTime = DateTime.UtcNow;

            var hrBrasilia = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");

            return TimeZoneInfo.ConvertTimeFromUtc(dateTime, hrBrasilia);
        }

        public static DateTime ObterHorarioBrasilia(this DateTime dateTime)
        {
            var hrBrasilia = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");

            return TimeZoneInfo.ConvertTimeFromUtc(dateTime, hrBrasilia);
        }

        public static DateTime RetornarDateTime(this string data)
        {
            // função para retornar um campo DateTime a partir de
            // uma data string no formato DD/MM/AAAA
            // e no formato DD/MM/AAAA HH:MM

            DateTime dateTime = DateTime.MinValue;

            int dia = 0;
            int mes = 0;
            int ano = 0;
            int hora = 0;
            int minuto = 0;
            int segundo = 0;

            dia = Convert.ToInt32(data.Substring(0, 2));
            mes = Convert.ToInt32(data.Substring(3, 2));
            ano = Convert.ToInt32(data.Substring(6, 4));

            if (data.Length == 16)
            {
                hora = Convert.ToInt32(data.Substring(11, 2));
                minuto = Convert.ToInt32(data.Substring(14, 2));

                dateTime = new DateTime(ano, mes, dia, hora, minuto, 0);
            }

            if (data.Length == 19)
            {
                hora = Convert.ToInt32(data.Substring(11, 2));
                minuto = Convert.ToInt32(data.Substring(14, 2));
                segundo = Convert.ToInt32(data.Substring(17, 2));

                dateTime = new DateTime(ano, mes, dia, hora, minuto, segundo);
            }

            return dateTime;
        }
    }
}