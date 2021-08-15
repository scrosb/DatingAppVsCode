using System;

namespace API.Extensions
{
    public static class DateTimeExtensions
    {
        public static int CalculateAge(this DateTime dob)
        {
            var today = DateTime.Today;
            var age = today.Year - dob.Year;
            //Haven't had their birthday yet this year
            if (dob.Date > today.AddYears(-age)) age--;
            return age;
        }
    }
}