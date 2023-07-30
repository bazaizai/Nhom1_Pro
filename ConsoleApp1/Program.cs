using System.Linq;

string firstName = "Chang";
string lastName = "Nguyen ";
string middleName = "Phuong";
string fullName = (lastName, " ", middleName, " ", firstName);

string subString = fullName.Substring(11, 16);

string Contains = Contains("Chang", fullName);

string Distinct = Distinct("Chang");

Console.WriteLine(fullName, Contains, Distinct, subString);
