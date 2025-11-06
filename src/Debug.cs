using Landis.Library.UniversalCohorts;
using Landis.SpatialModeling;
using log4net;

namespace Landis.Library.BiomassHarvest
{
    /// <summary>
    /// Helper methods for logging debug information
    /// </summary>
    public static class Debug
    {
        private static ISiteVar<SiteCohorts> cohorts;

        /// <summary>
        /// Write the list of cohorts at a site to a log.
        /// </summary>
        public static void WriteSiteCohorts(ILog       log,
                                            ActiveSite site)
        {
            if (cohorts == null)
                cohorts = Model.Core.GetSiteVar<SiteCohorts>("Succession.UniversalCohorts");

            int count = 0;  // # of species with cohorts
            foreach (ISpeciesCohorts speciesCohorts in cohorts[site])
            {
                string cohort_list = "";
                foreach (ICohort cohort in speciesCohorts)
                {
                    cohort_list += string.Format(", {0} yrs ({1})", cohort.Data.Age, cohort.Data.Biomass);
                }
                log.DebugFormat("      {0}{1}", speciesCohorts.Species.Name, cohort_list);
                count += 1;
            }
            if (count == 0)
                log.DebugFormat("      (no cohorts)");
        }
    }
}
