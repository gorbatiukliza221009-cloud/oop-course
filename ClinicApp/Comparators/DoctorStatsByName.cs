using ClinicApp.Models;

namespace ClinicApp.Comparators;

public class DoctorStatsByName : IComparer<DoctorStats>
{
    public int Compare(DoctorStats? x, DoctorStats? y)
    {
        if (x == null && y == null) return 0;
        if (x == null) return -1;
        if (y == null) return 1;

        // x з y — зростання: А → Я
        return string.Compare(x.FullName, y.FullName, StringComparison.CurrentCulture);
    }
}