using System;
using System.ComponentModel.DataAnnotations;

namespace ManyEvents.Models
{
	public class PersonalCode
	{
		[Key]
		[StringLength(11)]
		public string Code { get; set; }

		public PersonalCode(string code)
		{
			if(ValidateIDCode(code))
			{
                Code = code;
            }
			else
			{
				Code = "";
			}
			
		}

        private bool ValidateIDCode(string idCode)
        {
            try
            {
                // check length
                if (idCode.Length != 11) return false;
                int century = 0;

                // check century
                switch (idCode[0])
                {
                    case '1':
                    case '2':
                        {
                            century = 1800;
                            break;
                        }
                    case '3':
                    case '4':
                        {
                            century = 1900;
                            break;
                        }
                    case '5':
                    case '6':
                        {
                            century = 2000;
                            break;  
                        }
                    default:
                        {
                            return false;
                        }
                }

                // check if birthday is a valid date
                // get a date from IK
                string s = idCode.Substring(5, 2) + "." +

                    idCode.Substring(3, 2) + "." +
                    Convert.ToString(century + Convert.ToInt32(idCode.Substring(1, 2)));

                //error if parse fails, catch gets false. TryParse() does not exist in .NET 1.1
                DateTime d = DateTime.Parse(s);

                // calculate the checksum
                int n = Int16.Parse(idCode[0].ToString()) * 1
                      + Int16.Parse(idCode[1].ToString()) * 2
                      + Int16.Parse(idCode[2].ToString()) * 3
                      + Int16.Parse(idCode[3].ToString()) * 4
                      + Int16.Parse(idCode[4].ToString()) * 5
                      + Int16.Parse(idCode[5].ToString()) * 6
                      + Int16.Parse(idCode[6].ToString()) * 7
                      + Int16.Parse(idCode[7].ToString()) * 8
                      + Int16.Parse(idCode[8].ToString()) * 9
                      + Int16.Parse(idCode[9].ToString()) * 1;

                int c = n % 11;

                // special case recalculate the checksum
                if (c == 10)
                {

                    // calculate the checksum
                    n = Int16.Parse(idCode[0].ToString()) * 3
                      + Int16.Parse(idCode[1].ToString()) * 4
                      + Int16.Parse(idCode[2].ToString()) * 5
                      + Int16.Parse(idCode[3].ToString()) * 6
                      + Int16.Parse(idCode[4].ToString()) * 7
                      + Int16.Parse(idCode[5].ToString()) * 8
                      + Int16.Parse(idCode[6].ToString()) * 9
                      + Int16.Parse(idCode[7].ToString()) * 1
                      + Int16.Parse(idCode[8].ToString()) * 2
                      + Int16.Parse(idCode[9].ToString()) * 3;

                    c = n % 11;
                    c = c % 10;

                }

                return (c == Int16.Parse(idCode[10].ToString()));

            }
            catch
            {
                return false;
            }
        }

    }
}

