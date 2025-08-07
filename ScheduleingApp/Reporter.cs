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

        public (string Type, int Count) MostCommonAppointment(List<Appointment> appointments) =>
            appointments
                .GroupBy(a => a.Type)
                .OrderByDescending(g => g.Count())
                .Select(g => (g.Key, g.Count()))
                .FirstOrDefault();

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
                    report.AppendLine($"    {appt.Start.ToShortDateString()} - {appt.Type}");
            }

            var mostCommonAppointment = MostCommonAppointment(appointments);
            report.AppendLine("\nMost Common Appointment Type:");
            report.AppendLine($"  {mostCommonAppointment.Type} ({mostCommonAppointment.Count})");

            return report.ToString();
        }
    }

}
