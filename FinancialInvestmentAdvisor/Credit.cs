using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetCreditScore
{
    public class Credit
    {
        public string StatusOfCheckingAccount { get; set; }
        public string CreditHistory { get; set; }
        public string StatusOfSavingsAccount { get; set; }
        public string PresentEmploymentSince { get; set; }
        public string PersonalStatusAndSex { get; set; }
        public string Property { get; set; }
        public int Age { get; set; }
        public string Housing { get; set; }
        public string Job { get; set; }
        public int NumberOfDependents { get; set; }
        public int CreditRating { get; set; }
        public int ScoredLabels { get; set; }
        public double ScoredProbabilities { get; set; }
    }
}
