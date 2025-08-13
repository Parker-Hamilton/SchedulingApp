using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleingApp
{
    public class Reporter
    {
        public Dictionary<string, Dictionary<string, int>> NumberOfAppointmentsByMonth(List<Appointment> appointments) =>
            appointments
                .GroupBy(a => a.Start.ToString("MMMM"))
                .ToDictionary(
                    g => g.Key,
                    g => g.GroupBy(a => a.Type)
                          .ToDictionary(t => t.Key, t => t.Count())
                );

        public Dictionary<string, List<Appointment>> ScheduleByUser(List<Appointment> appointments) =>
            appointments
                .GroupBy(a => a.UserName)
                .ToDictionary(
                    g => g.Key,
                    g => g.ToList()
                );

        public HashSet<(string Type, int Count)> MostCommonAppointments(List<Appointment> appointments)
        {
            var types = appointments
                .GroupBy(a => a.Type)
                .Select(g => new { Type = g.Key, Count = g.Count() })
                .ToList();

            int maxCount = types.Max(g => g.Count);

            var mostCommonTypes = types
                .Where(g => g.Count == maxCount)
                .Select(g => (g.Type, g.Count))
                .ToHashSet();

            return mostCommonTypes;

        }

        public string GenerateReportText(List<Appointment> appointments)
        {
            var report = new StringBuilder();

            var appointmentsByMonth = NumberOfAppointmentsByMonth(appointments);
            report.AppendLine("Appointments by Month:");
            foreach (var month in appointmentsByMonth)
            {
                report.AppendLine($"  {month.Key}:");
                foreach (var type in month.Value)
                    report.AppendLine($"    {type.Key}: {type.Value}");
            }

            var appointmentsByUser = ScheduleByUser(appointments);
            report.AppendLine("\nSchedule by User:");
            foreach (var user in appointmentsByUser)
            {
                report.AppendLine($"  {user.Key}:");
                foreach (var appt in user.Value)
                    report.AppendLine($"    {appt.Start.ToString("MMM dd hh:mm tt")} - {appt.Type}");
            }

            var mostCommonAppointment = MostCommonAppointments(appointments);
            if (mostCommonAppointment.Count > 1)
            {
                report.AppendLine("\nMost Common Appointment Types (a tie occurred, all types that appear the most will be listed):");
            }
            else
            {
                report.AppendLine("\nMost Common Appointment Type:");
            }
            foreach (var app in mostCommonAppointment)
            {
                report.AppendLine($"	{app.Type} ({app.Count})");
            }


            return report.ToString();
        }
    }

}
