using parser.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hakaton.Application.Parser
{
    public class ParseManager
    {
        private readonly IAirportCodeResolver airportCodeResolver;
        private readonly IAirlineCodeResolver airlineCodeResolver;

        public ParseManager(IAirportCodeResolver airportCodeResolver, IAirlineCodeResolver airlineCodeResolver)
        {
            this.airportCodeResolver = airportCodeResolver;
            this.airlineCodeResolver = airlineCodeResolver;
        }

        public string Parse(string ndpString)
        {
            string[] parts = ndpString.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            //TS 275 J 15OCT 4 LGWYVR HK1 1010 1200 332 E 0
            // 0   1 2  3    4  5      6   7    8    9  10  11
            //15 окт., 10:10 – 12:00, Лондон (Гатвик) — Ванкувер, TS 275, Air Transat
            if ((parts.Length != 12 ) || (parts[0].Length != 2) || (parts[1].Length >4) ||
                (parts[3].Length !=5) || (parts[4].Length !=1) || (parts[5].Length !=6) ||
                (parts[7].Length !=4) || (parts[8].Length !=4))
            {
                throw new Exception("Input is incorrect!");
            }
            string airlineCode = parts[0];
            string flightNumber = parts[1];
            string departureDayOfWeek = GetDayOfWeek(parts[4]);
            string departureDate = ParseDate(parts[3]);
            string departureAirportCode = parts[5].Substring(0,3);
            string arrivalAirportCode = parts[5].Substring(3,3);
            string departureTime = ParseTime(parts[7]);
            string arrivalTime = ParseTime(parts[8]);

            string departureAirport = airportCodeResolver.GetAirportName(departureAirportCode);
            string arrivalAirport = airportCodeResolver.GetAirportName(arrivalAirportCode);
            string airlineName = airlineCodeResolver.GetAirlineName(airlineCode);

            return $"{ParseDate(departureDate)} , {departureTime} - {arrivalTime}, {departureAirport} - {arrivalAirport}, {airlineCode} {flightNumber}, {airlineName}";
        }

        private string ParseDate(string dateString)
        {
            string dayOfMonth = dateString.Substring(0, 2);
            string month = GetMonthName(dateString.Substring(2, 3));
            return $"{dayOfMonth} {month}.";
        }

        private string ParseTime(string timeString)
        {
            string hours = timeString.Substring(0, 2);
            string minutes = timeString.Substring(2, 2);
            return $"{hours}:{minutes}";
        }

        private string GetMonthName(string monthCode)
        {
            switch (monthCode)
            {
                case "JAN":
                    return "янв";
                case "FEB":
                    return "фев";
                case "MAR":
                    return "март";
                case "APR":
                    return "апр";
                case "MAY":
                    return "май";
                case "JUN":
                    return "июнь";
                case "JUL":
                    return "июль";
                case "AUG":
                    return "авг";
                case "SEP":
                    return "сент";
                case "OCT":
                    return "окт";
                case "NOV":
                    return "нояб";
                case "DEC":
                    return "дек";
                default:
                    return monthCode;
            }
        }

        private string GetDayOfWeek(string dayOfWeekCode)
        {
            switch (dayOfWeekCode)
            {
                case "1":
                    return "Пон";
                case "2":
                    return "Вт";
                case "3":
                    return "Ср";
                case "4":
                    return "Ч";
                case "5":
                    return "Пт";
                case "6":
                    return "Сб";
                case "7":
                    return "Вс";
                default:
                    return dayOfWeekCode;
            }
        }
    }
}
