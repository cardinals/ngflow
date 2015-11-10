
using System;

namespace BP.Tools
{ 
	/// 
	///  Function : String functions  
	/// 
	public class DealString
	{ 
		#region  Private members  
		/// 
		///  Input string  
		/// 
		private string inputString=null; 
		/// 
		///  Output string  
		/// 
		private string outString=null; 
		/// 
		///  Message  
		/// 
		private string noteMessage=null; 
		#endregion 

		#region  Public property  
		/// 
		///  Input string  
		/// 
		public string InputString 
		{ 
			get{return inputString;} 
			set{inputString=value;} 
		} 
		/// 
		///  Output string  
		/// 
		public string OutString 
		{ 
			get{return outString;} 
			set{outString=value;} 
		} 
		/// 
		///  Message  
		/// 
		public string NoteMessage 
		{ 
			get{return noteMessage;} 
			set{noteMessage=value;} 
		} 
		#endregion 

		#region  Constructor  
        public DealString() 
		{ 
			// 
			// TODO:  Add constructor logic here  
			// 
		} 
		#endregion 

		#region  Public Methods  
		public void ConvertToChineseNum() 
		{ 
			string numList=" Zero One II Triple Stanford Lu Qi Wu Jiu Ba "; 
			string rmbList = " Sub-diagonal elements pick up pick up 250 million Bai Bai Bai pick thousand billion 250 million "; 
			double number=0; 
			string tempOutString=null; 

			try 
			{ 
				number=double.Parse(this.inputString); 
			} 
			catch 
			{ 
				this.noteMessage=" Non-numeric parameters passed !"; 
				return; 
			} 

			if(number>9999999999999.99) 
				this.noteMessage=" Beyond the scope of the RMB value "; 

			// Will be converted to decimal integer string  
			string tempNumberString=Convert.ToInt64(number*100).ToString(); 
			int tempNmberLength=tempNumberString.Length; 
			int i=0; 
			while( i<tempNmberLength ) 
			{ 
				int oneNumber=Int32.Parse(tempNumberString.Substring(i,1)); 
				string oneNumberChar=numList.Substring(oneNumber,1); 
				string oneNumberUnit=rmbList.Substring(tempNmberLength-i-1,1); 
				if(oneNumberChar!="��") 
					tempOutString+=oneNumberChar+oneNumberUnit; 
				else 
				{ 
					if(oneNumberUnit=="��"||oneNumberUnit=="��"||oneNumberUnit=="Ԫ"||oneNumberUnit=="��") 
					{ 
						while (tempOutString.EndsWith("��")) 
						{ 
							tempOutString=tempOutString.Substring(0,tempOutString.Length-1); 
						} 

					}
                    if (oneNumberUnit == "��" || (oneNumberUnit == "��" && !tempOutString.EndsWith("��")) || oneNumberUnit == "Ԫ")
                    {
                        tempOutString += oneNumberUnit;
                    }
                    else
                    {
                        bool tempEnd = tempOutString.EndsWith("��");
                        bool zeroEnd = tempOutString.EndsWith("��");
                        if (tempOutString.Length > 1)
                        {
                            bool zeroStart = tempOutString.Substring(tempOutString.Length - 2, 2).StartsWith("��");
                            if (!zeroEnd && (zeroStart || !tempEnd))
                                tempOutString += oneNumberChar;
                        }
                        else
                        {
                            if (!zeroEnd && !tempEnd)
                                tempOutString += oneNumberChar;
                        }
                    } 
				} 
				i+=1; 
			} 

			while (tempOutString.EndsWith("��")) 
			{ 
				tempOutString=tempOutString.Substring(0,tempOutString.Length-1); 
			} 

			while(tempOutString.EndsWith("Ԫ")) 
			{ 
				tempOutString=tempOutString+"��"; 
			} 

			this.outString=tempOutString; 


		} 
		#endregion 
	} 
} 
