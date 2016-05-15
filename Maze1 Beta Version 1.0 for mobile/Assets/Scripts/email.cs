using UnityEngine;
using System.Collections;
/*using System;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;*/

public class email : MonoBehaviour {

	public static string  r1Time;
	public static string  r2Time;
	public static string  r3Time;
	public static string  r4Time;
	public static string  r5Time;
	string body = "";
	string settings = "";

	string r1 = "INCOMPLETE:";
	string r2 = "INCOMPLETE:";
	string r3= "INCOMPLETE:";
	string r4= "INCOMPLETE:";
	string r5= "INCOMPLETE:";

	void Start()
	{
		Debug.Log ("Resetting time text");
		r1Time = "No Time";
		r2Time = "No Time";
		r3Time = "No Time";
		r4Time = "No Time";
		r5Time = "No Time";
	}
	/*public void Main ()
	{
		if (GameLogic.gameStarted) {
			MailMessage mail = new MailMessage ();

			mail.From = new MailAddress ("troublemakertechnologies.maze1@gmail.com");
			mail.To.Add ("coolsnt@gmail.com"); //User address
			mail.Subject = "Maze 1 testing apparatus results";



			if (UIManagerScript.roundNum == 1)
				body = "\nRound 1 " +r1+ r1Time;
			else if (UIManagerScript.roundNum == 2)
				body = "\nRound 1 "+r1+ r1Time + "\nRound 2 "+r2+ r2Time;
			else if (UIManagerScript.roundNum == 3)
				body = "\nRound 1 " +r1+ r1Time + "\nRound 2 " +r2+ r2Time + "\nRound 3 " +r3+ r3Time;
			else if (UIManagerScript.roundNum == 4)
				body = "\nRound 1 " +r1+ r1Time+ "\nRound 2 " +r2+ r2Time + "\nRound 3 " +r3+ r3Time + "\nRound 4 " +r4+ r4Time;
			else if (UIManagerScript.roundNum == 5)
				body = "\nRound 1 " +r1+ r1Time+ "\nRound 2 " +r2+ r2Time + "\nRound 3 " +r3+ r3Time + "\nRound 4 " +r4+ r4Time + "\nRound 5 " + r5+r5Time;

			settings = "Number of rounds: " + UIManagerScript.roundNum+ "\nMaze traversed: "+selectManager.mazeText+"\nMarkers: "+UIManagerScript.markerTxt;
		
			mail.Body = "Hello user! The following is a summary of your experience in the Maze 1 testing environment: \n\nSettings: \n\n" +settings+ "\n\nData: \n"+body;

			SmtpClient smtpServer = new SmtpClient ("smtp.gmail.com");
			smtpServer.Port = 587;
			smtpServer.Credentials = new System.Net.NetworkCredential ("troublemakertechnologies.maze1@gmail.com", "cooldude9") as ICredentialsByHost;
			smtpServer.EnableSsl = true;
			ServicePointManager.ServerCertificateValidationCallback = 
			delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) {
				return true;
			};

			//string attachmentPath = @"C:\somefile.txt";
		//System.Net.Mail.Attachment attachment = new System.Net.Mail.Attachment(attachmentPath);
		//mail.Attachments.Add(attachment);

			smtpServer.Send (mail);
			Debug.Log ("success");
		}

	}*/

	void Update()
	{
		if (GameLogic.r1Complete)
			r1 = "COMPLETE:";
		if (GameLogic.r2Complete)
			r2 = "COMPLETE:";
		if (GameLogic.r3Complete)
			r3 = "COMPLETE:";
		if (GameLogic.r4Complete)
			r4 = "COMPLETE:";
		if (GameLogic.r5Complete)
			r5 = "COMPLETE:";
	}
}
