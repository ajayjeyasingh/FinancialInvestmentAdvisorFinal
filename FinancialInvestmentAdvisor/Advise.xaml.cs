using GetCreditScore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using System.Configuration;

namespace FinancialInvestmentAdvisor
{
    /// <summary>
    /// Interaction logic for Advise.xaml
    /// </summary>
    public partial class Advise : MetroWindow
    {
        public Advise()
        {
            InitializeComponent();
            LoadData();
        }

        public int CreditScore { get; set; }

        private void LoadData()
        {
            List<BaseDTO> existingAcctStatus = new List<BaseDTO>();
            existingAcctStatus.Add(new BaseDTO() { Id="-1",DisplayName="-Select-" });
            existingAcctStatus.Add(new BaseDTO() { Id = "A11", DisplayName = "< 0" });
            existingAcctStatus.Add(new BaseDTO() { Id = "A12", DisplayName = "0 <= 200,000" });
            existingAcctStatus.Add(new BaseDTO() { Id = "A13", DisplayName = ">= 200,000" });
            existingAcctStatus.Add(new BaseDTO() { Id = "A14", DisplayName = "No Checking Acount" });
            cbExistingAcctStatus.DisplayMemberPath = "DisplayName";
            cbExistingAcctStatus.SelectedValuePath = "Id";
            cbExistingAcctStatus.ItemsSource = existingAcctStatus;

            List<BaseDTO> savingsAcct = new List<BaseDTO>();
            savingsAcct.Add(new BaseDTO() { Id = "-1", DisplayName = "-Select-" });
            savingsAcct.Add(new BaseDTO() { Id = "A61", DisplayName = "< 100,000" });
            savingsAcct.Add(new BaseDTO() { Id = "A62", DisplayName = "100,000 <= 500,000" });
            savingsAcct.Add(new BaseDTO() { Id = "A63", DisplayName = "500,000 <= 1,000,000" });
            savingsAcct.Add(new BaseDTO() { Id = "A64", DisplayName = ">= 1,000,000" });
            savingsAcct.Add(new BaseDTO() { Id = "A65", DisplayName = "Unknown/No Savings Account" });
            cbSavingsAcct.DisplayMemberPath = "DisplayName";
            cbSavingsAcct.SelectedValuePath = "Id";
            cbSavingsAcct.ItemsSource = savingsAcct;

            List<BaseDTO> creditHistory = new List<BaseDTO>();
            creditHistory.Add(new BaseDTO() { Id = "-1", DisplayName = "-Select-" });
            creditHistory.Add(new BaseDTO() { Id = "A30", DisplayName = "No credits taken/ all credits paid back duly" });
            creditHistory.Add(new BaseDTO() { Id = "A31", DisplayName = "All credits at this bank paid back duly" });
            creditHistory.Add(new BaseDTO() { Id = "A32", DisplayName = "Existing credits paid back duly till now" });
            creditHistory.Add(new BaseDTO() { Id = "A33", DisplayName = "Delay in paying off in the past" });
            creditHistory.Add(new BaseDTO() { Id = "A34", DisplayName = "Critical account/ other credits existing" });
            cbCreditHistory.DisplayMemberPath = "DisplayName";
            cbCreditHistory.SelectedValuePath = "Id";
            cbCreditHistory.ItemsSource = creditHistory;

            List<BaseDTO> presentEmplmntSince = new List<BaseDTO>();
            presentEmplmntSince.Add(new BaseDTO() { Id = "-1", DisplayName = "-Select-" });
            presentEmplmntSince.Add(new BaseDTO() { Id = "A71", DisplayName = "Unemployed" });
            presentEmplmntSince.Add(new BaseDTO() { Id = "A72", DisplayName = "< 1 year" });
            presentEmplmntSince.Add(new BaseDTO() { Id = "A73", DisplayName = "1 <= 4 years" });
            presentEmplmntSince.Add(new BaseDTO() { Id = "A74", DisplayName = "4 <= 7 years" });
            presentEmplmntSince.Add(new BaseDTO() { Id = "A75", DisplayName = "> 7 years" });
            cbPresentEmplmntSince.DisplayMemberPath = "DisplayName";
            cbPresentEmplmntSince.SelectedValuePath = "Id";
            cbPresentEmplmntSince.ItemsSource = presentEmplmntSince;

            List<BaseDTO> personalStatus = new List<BaseDTO>();
            personalStatus.Add(new BaseDTO() { Id = "-1", DisplayName = "-Select-" });
            personalStatus.Add(new BaseDTO() { Id = "A91", DisplayName = "Male: Divorced/Separated" });
            personalStatus.Add(new BaseDTO() { Id = "A92", DisplayName = "Female: Divorced/Separated/Married" });
            personalStatus.Add(new BaseDTO() { Id = "A93", DisplayName = "Male: Single" });
            personalStatus.Add(new BaseDTO() { Id = "A94", DisplayName = "Male: Married/Widowed" });
            personalStatus.Add(new BaseDTO() { Id = "A95", DisplayName = "Female: Single" });
            cbPersonalStatus.DisplayMemberPath = "DisplayName";
            cbPersonalStatus.SelectedValuePath = "Id";
            cbPersonalStatus.ItemsSource = personalStatus;

            List<BaseDTO> property = new List<BaseDTO>();
            property.Add(new BaseDTO() { Id = "-1", DisplayName = "-Select-" });
            property.Add(new BaseDTO() { Id = "A121", DisplayName = "Real Estate" });
            property.Add(new BaseDTO() { Id = "A122", DisplayName = "Building Society Savings Agreement/Life Insurance" });
            property.Add(new BaseDTO() { Id = "A123", DisplayName = "Car or Other" });
            property.Add(new BaseDTO() { Id = "A124", DisplayName = "Unknown/No Property" });
            cbProperty.DisplayMemberPath = "DisplayName";
            cbProperty.SelectedValuePath = "Id";
            cbProperty.ItemsSource = property;

            List<BaseDTO> Housing = new List<BaseDTO>();
            Housing.Add(new BaseDTO() { Id = "-1", DisplayName = "-Select-" });
            Housing.Add(new BaseDTO() { Id = "A151", DisplayName = "Rent" });
            Housing.Add(new BaseDTO() { Id = "A152", DisplayName = "Own" });
            Housing.Add(new BaseDTO() { Id = "A153", DisplayName = "For Free" });
            cbHousing.DisplayMemberPath = "DisplayName";
            cbHousing.SelectedValuePath = "Id";
            cbHousing.ItemsSource = Housing;

            List<BaseDTO> job = new List<BaseDTO>();
            job.Add(new BaseDTO() { Id = "-1", DisplayName = "-Select-" });
            job.Add(new BaseDTO() { Id = "A171", DisplayName = "Unemployed/Unskilled-Non-resident" });
            job.Add(new BaseDTO() { Id = "A172", DisplayName = "Unskilled-Resident" });
            job.Add(new BaseDTO() { Id = "A173", DisplayName = "Skilled Employee/Official" });
            job.Add(new BaseDTO() { Id = "A174", DisplayName = "Management/Self-Employed/Highly Qualified Employee/Officer" });
            cbJob.DisplayMemberPath = "DisplayName";
            cbJob.SelectedValuePath = "Id";
            cbJob.ItemsSource = job;
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            Clear();
        }

        private void btnAdvise_Click(object sender, RoutedEventArgs e)
        {
            if (IsValid())
            {
                Credit creditData = new Credit();
                creditData.StatusOfCheckingAccount = cbExistingAcctStatus.SelectedValue.ToString();
                creditData.StatusOfSavingsAccount = cbSavingsAcct.SelectedValue.ToString();
                creditData.PresentEmploymentSince = cbPresentEmplmntSince.SelectedValue.ToString();
                creditData.PersonalStatusAndSex = cbPersonalStatus.SelectedValue.ToString();
                creditData.Property = cbProperty.SelectedValue.ToString();
                creditData.Age = Convert.ToInt16(tbAge.Text.Trim());
                creditData.Housing= cbHousing.SelectedValue.ToString();
                creditData.Job= cbJob.SelectedValue.ToString();
                creditData.CreditHistory = cbCreditHistory.SelectedValue.ToString();
                creditData.NumberOfDependents = Convert.ToInt16(tbDependents.Text.Trim());
                InvokeRequestResponseService(creditData);
                if(CreditScore == 1 && creditData.StatusOfSavingsAccount == "A64")
                {
                    Clear();
                    spInvestmentHigh.Visibility = Visibility.Collapsed;
                    spInvestmentLow.Visibility = Visibility.Collapsed;
                    spInvestmentHigh10LakhPlus.Visibility = Visibility.Visible;
                    spInvestmentHigh5LakhPlus.Visibility = Visibility.Collapsed;
                    spInvestmentHigh1LakhPlus.Visibility = Visibility.Collapsed;
                    spInvestmentHigh1LakhMinus.Visibility = Visibility.Collapsed;
                }
                else if (CreditScore == 1 && creditData.StatusOfSavingsAccount == "A63")
                {
                    Clear();
                    spInvestmentHigh.Visibility = Visibility.Collapsed;
                    spInvestmentLow.Visibility = Visibility.Collapsed;
                    spInvestmentHigh10LakhPlus.Visibility = Visibility.Collapsed;
                    spInvestmentHigh5LakhPlus.Visibility = Visibility.Visible;
                    spInvestmentHigh1LakhPlus.Visibility = Visibility.Collapsed;
                    spInvestmentHigh1LakhMinus.Visibility = Visibility.Collapsed;
                }
                else if (CreditScore == 1 && creditData.StatusOfSavingsAccount == "A62")
                {
                    Clear();
                    spInvestmentHigh.Visibility = Visibility.Collapsed;
                    spInvestmentLow.Visibility = Visibility.Collapsed;
                    spInvestmentHigh10LakhPlus.Visibility = Visibility.Collapsed;
                    spInvestmentHigh5LakhPlus.Visibility = Visibility.Collapsed;
                    spInvestmentHigh1LakhPlus.Visibility = Visibility.Visible;
                    spInvestmentHigh1LakhMinus.Visibility = Visibility.Collapsed;
                }
                else if (CreditScore == 1 && creditData.StatusOfSavingsAccount == "A61")
                {
                    Clear();
                    spInvestmentHigh.Visibility = Visibility.Collapsed;
                    spInvestmentLow.Visibility = Visibility.Collapsed;
                    spInvestmentHigh10LakhPlus.Visibility = Visibility.Collapsed;
                    spInvestmentHigh5LakhPlus.Visibility = Visibility.Collapsed;
                    spInvestmentHigh1LakhPlus.Visibility = Visibility.Collapsed;
                    spInvestmentHigh1LakhMinus.Visibility = Visibility.Visible;
                }
                else if (CreditScore == 1 && creditData.StatusOfSavingsAccount == "A65")
                {
                    Clear();
                    spInvestmentHigh.Visibility = Visibility.Visible;
                    spInvestmentLow.Visibility = Visibility.Collapsed;
                    spInvestmentHigh10LakhPlus.Visibility = Visibility.Collapsed;
                    spInvestmentHigh5LakhPlus.Visibility = Visibility.Collapsed;
                    spInvestmentHigh1LakhPlus.Visibility = Visibility.Collapsed;
                    spInvestmentHigh1LakhMinus.Visibility = Visibility.Collapsed;
                }
                else if(CreditScore == 2)
                {
                    Clear();
                    spInvestmentHigh.Visibility = Visibility.Collapsed;
                    spInvestmentLow.Visibility = Visibility.Visible;
                    spInvestmentHigh10LakhPlus.Visibility = Visibility.Collapsed;
                    spInvestmentHigh5LakhPlus.Visibility = Visibility.Collapsed;
                    spInvestmentHigh1LakhPlus.Visibility = Visibility.Collapsed;
                    spInvestmentHigh1LakhMinus.Visibility = Visibility.Collapsed;
                }
                else if (CreditScore == -1)
                {
                    System.Windows.Forms.MessageBox.Show("Service Error");
                }
                spCapture.Visibility = Visibility.Collapsed;
            }
            else
            {
                MessageBox.Show("All fields are mandatory.");
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            spCapture.Visibility = Visibility.Visible;
            spInvestmentHigh.Visibility = Visibility.Collapsed;
            spInvestmentLow.Visibility = Visibility.Collapsed;
            spInvestmentHigh10LakhPlus.Visibility = Visibility.Collapsed;
            spInvestmentHigh5LakhPlus.Visibility = Visibility.Collapsed;
            spInvestmentHigh1LakhPlus.Visibility = Visibility.Collapsed;
            spInvestmentHigh1LakhMinus.Visibility = Visibility.Collapsed;
        }

        private bool IsValid()
        {
            bool isValid = true;
            if (tbName.Text.Trim().Length == 0
                || cbExistingAcctStatus.SelectedIndex == 0
                || cbSavingsAcct.SelectedIndex == 0
                || cbCreditHistory.SelectedIndex == 0
                || cbPresentEmplmntSince.SelectedIndex == 0
                || cbPersonalStatus.SelectedIndex == 0
                || cbProperty.SelectedIndex == 0
                || tbAge.Text.Trim().Length == 0
                || cbHousing.SelectedIndex == 0
                || cbJob.SelectedIndex == 0
                || tbDependents.Text.Trim().Length == 0)
            {
                isValid = false;
            }
            return isValid;
        }

        private void Clear()
        {
            tbName.Text = string.Empty;
            cbExistingAcctStatus.SelectedIndex = 0;
            cbCreditHistory.SelectedIndex = 0;
            cbSavingsAcct.SelectedIndex = 0;
            cbPresentEmplmntSince.SelectedIndex = 0;
            cbPersonalStatus.SelectedIndex = 0;
            cbProperty.SelectedIndex = 0;
            tbAge.Text = string.Empty;
            cbHousing.SelectedIndex = 0;
            cbJob.SelectedIndex = 0;
            tbDependents.Text = string.Empty;
        }

        public void InvokeRequestResponseService(Credit credit)
        {
            CreditScore = -1;
            using (var client = new HttpClient())
            {
                var scoreRequest = new
                {
                    Inputs = new Dictionary<string, List<Dictionary<string, string>>>() {
                        {
                            "input1",
                            new List<Dictionary<string, string>>(){new Dictionary<string, string>(){
                                            {
                                                "Status Of Checking Account", credit.StatusOfCheckingAccount
                                            },
                                            {
                                                "Credit History", credit.CreditHistory
                                            },
                                            {
                                                "Status of Savings Account", credit.StatusOfSavingsAccount
                                            },
                                            {
                                                "Present Employment Since", credit.PresentEmploymentSince
                                            },
                                            {
                                                "Personal status and sex", credit.PersonalStatusAndSex
                                            },
                                            {
                                                "Property", credit.Property
                                            },
                                            {
                                                "Age", credit.Age.ToString()
                                            },
                                            {
                                                "Housing", credit.Housing
                                            },
                                            {
                                                "Job", credit.Job
                                            },
                                            {
                                                "Number of Dependents", credit.NumberOfDependents.ToString()
                                            },
                                            {
                                                "Credit Rating", "0"
                                            },
                                }
                            }
                        },
                    },
                    GlobalParameters = new Dictionary<string, string>()
                    {
                    }
                };

                string apiKey = ConfigurationManager.AppSettings["ApiKey"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["ApiURL"]);

                HttpResponseMessage response = client.PostAsJsonAsync("", scoreRequest).Result;


                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;

                    JObject joResult = JObject.Parse(result);
                    JObject joResults = (JObject)joResult["Results"];
                    JArray joOutput1 = (JArray)joResults["output1"];


                    dynamic dynObj = JsonConvert.DeserializeObject(joOutput1[0].ToString());

                    CreditScore = Convert.ToInt16(dynObj["Scored Labels"]);
                }
            }
        }
    }
}
