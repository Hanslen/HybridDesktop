using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using Leap;

namespace ConsoleApplication1
{
	class SampleListener:Listener{
		private Object thisLock = new Object();
		TcpListener serverSocket = new TcpListener(8888);
		TcpClient clientSocket = default(TcpClient);
		float start = 0;
		float[] fingerstatus = new float[10];
		int currentindex = 1;
		int updateinstruction = 0;
		int status = 1;
		private void SafeWriteLine (String line)
		{
			lock (thisLock) {
				Console.WriteLine (line);
			}
		}

		public override void OnInit (Controller controller)
		{
			SafeWriteLine ("Server and Leapmotion Initialized");

			// server start
			serverSocket.Start();
			SafeWriteLine(" >> Server Started");
			clientSocket = serverSocket.AcceptTcpClient();
			SafeWriteLine(" >> Accept connection from client");
		}
		// Leapmotion Connect
		public override void OnConnect (Controller controller)
		{
			SafeWriteLine ("Leapmotion Connected");
			controller.EnableGesture (Gesture.GestureType.TYPE_CIRCLE);
			controller.EnableGesture (Gesture.GestureType.TYPE_KEY_TAP);
			controller.EnableGesture (Gesture.GestureType.TYPE_SCREEN_TAP);
			controller.EnableGesture (Gesture.GestureType.TYPE_SWIPE);

		}

		public override void OnDisconnect (Controller controller)
		{
	        //Note: not dispatched when running in a debugger.
			SafeWriteLine ("Disconnected");
		}

		public override void OnExit (Controller controller)
		{
			SafeWriteLine ("Leapmotion Exited");

			clientSocket.Close();
			serverSocket.Stop();
			SafeWriteLine(" >>Server exit");
			Console.ReadLine();
		}

		// check whether it is an increase array
		public bool checkincrease(float[] arr){
			for(int i = 1; i < 9; i++){
				if(arr[i] <= arr[i-1]){
					return false;
				}
			}
			return true;
		}

		// check whether it is a decrease array
		public bool checkdecrease(float[] arr){
			for(int i = 1; i < 9; i++){
				if(arr[i] >= arr[i-1]){
					return false;
				}
			}
			return true;
		}
		public void printArray(float[] arr){
			SafeWriteLine("Printing:");
			for(int i = 1; i< 10; i++){
				SafeWriteLine(currentindex+"index"+arr[i]);
			}
			SafeWriteLine("PrintingEnd");
		}

		// This function will be called continuously during the server is running
		public override void OnFrame(Controller controller){

			// SafeWriteLine("Frame is working now.");
			// fingerstatus[currentindex] = 10000;
			
					// SafeWriteLine ("Frame id: " + frame.Id
  //                   + ", timestamp: " + frame.Timestamp
  //                   + ", hands: " + frame.Hands.Count
  //                   + ", fingers: " + frame.Fingers.Count
  //                   + ", tools: " + frame.Tools.Count
  //                   + ", gestures: " + frame.Gestures ().Count);
			// get basic hand position
			Frame frame = controller.Frame ();
			foreach (Hand hand in frame.Hands) {
				// SafeWriteLine("X:"+hand.PalmPosition.x+"Y:"+hand.PalmPosition.y+"Z:"+hand.PalmPosition.z);
				// SafeWriteLine("Y:"+hand.PalmPosition.y+" status"+status);
				if(status == 2){
					if(hand.PalmPosition.z < -50){
						SafeWriteLine("Move Back........");
						String textinput;
						textinput = "MoveBack";
						NetworkStream serverStream = clientSocket.GetStream ();
						byte[] outStream = System.Text.Encoding.ASCII.GetBytes (textinput + "$");
						serverStream.Write (outStream, 0, outStream.Length);
						serverStream.Flush ();
					}
					else if(hand.PalmPosition.z > 110){
						SafeWriteLine("Move to front......");
						String textinput;
						textinput = "MoveFront";
						NetworkStream serverStream = clientSocket.GetStream ();
						byte[] outStream = System.Text.Encoding.ASCII.GetBytes (textinput + "$");
						serverStream.Write (outStream, 0, outStream.Length);
						serverStream.Flush ();
					}
					else if(hand.PalmPosition.x < -80){
						SafeWriteLine("Move to Left......");
						String textinput;
						textinput = "MoveLeft";
						NetworkStream serverStream = clientSocket.GetStream ();
						byte[] outStream = System.Text.Encoding.ASCII.GetBytes (textinput + "$");
						serverStream.Write (outStream, 0, outStream.Length);
						serverStream.Flush ();
					}
					else if(hand.PalmPosition.x > 80){
						SafeWriteLine("Move to right......");
						String textinput;
						textinput = "MoveRight";
						NetworkStream serverStream = clientSocket.GetStream ();
						byte[] outStream = System.Text.Encoding.ASCII.GetBytes (textinput + "$");
						serverStream.Write (outStream, 0, outStream.Length);
						serverStream.Flush ();
					}
					else{
						SafeWriteLine("X:"+hand.PalmPosition.x+" "+"Z:"+hand.PalmPosition.z);
						String textinput;
						textinput = "X:"+hand.PalmPosition.x+" "+"Z:"+hand.PalmPosition.z;
						NetworkStream serverStream = clientSocket.GetStream ();
						byte[] outStream = System.Text.Encoding.ASCII.GetBytes (textinput + "$");
						serverStream.Write (outStream, 0, outStream.Length);
						serverStream.Flush ();
					}
				}
				if(hand.PalmPosition.y < 150 && status == 1){
					// SafeWriteLine("0.0"+hand.PalmPosition.y);	
					fingerstatus[currentindex] = hand.PalmPosition.x;
					currentindex += 1;
					if(currentindex == 10){
						currentindex = 0;
						for(int i = 0; i < 10; i++){
							fingerstatus[i] = 0;
						}
					}
					// printArray(fingerstatus);
					if(checkdecrease(fingerstatus)){
						currentindex = 0;
						for(int i = 0; i < 10; i++){
							fingerstatus[i] = 0;
						}
						start = hand.PalmPosition.x;
						SafeWriteLine("Previous");
						String textinput;
						textinput = "Previous";
						NetworkStream serverStream = clientSocket.GetStream ();
						byte[] outStream = System.Text.Encoding.ASCII.GetBytes (textinput + "$");
						serverStream.Write (outStream, 0, outStream.Length);
						serverStream.Flush ();
					}
					else if(checkincrease(fingerstatus)){
						currentindex = 0;
						for(int i = 0; i < 10; i++){
							fingerstatus[i] = 0;
						}
						start = hand.PalmPosition.x;
						SafeWriteLine("Next");
						String textinput;
						textinput = "Next";
						NetworkStream serverStream = clientSocket.GetStream ();
						byte[] outStream = System.Text.Encoding.ASCII.GetBytes (textinput + "$");
						serverStream.Write (outStream, 0, outStream.Length);
						serverStream.Flush ();
					}
				}
				else if(hand.PalmPosition.y > 250){
					updateinstruction += 1;
					if(updateinstruction == 200){
						SafeWriteLine("Chaging status");
						String textinput;
						textinput = "updateinstruction";
						NetworkStream serverStream = clientSocket.GetStream ();
						byte[] outStream = System.Text.Encoding.ASCII.GetBytes (textinput + "$");
						serverStream.Write (outStream, 0, outStream.Length);
						serverStream.Flush ();
						updateinstruction = 0;
						if(status == 1){
							status = 2;
						}
						else if(status == 2){
							status = 1;
						}
					}
					else if(updateinstruction%20 == 0){
						String textinput;
						textinput = updateinstruction/2+"%";
						NetworkStream serverStream = clientSocket.GetStream ();
						byte[] outStream = System.Text.Encoding.ASCII.GetBytes (textinput + "$");
						serverStream.Write (outStream, 0, outStream.Length);
						serverStream.Flush ();
						SafeWriteLine(updateinstruction/2+"%");
					}
					
				}
			}

			// Get gestures
			if(status == 2){
				GestureList gestures = frame.Gestures ();
				for (int i = 0; i < gestures.Count; i++) {
					Gesture gesture = gestures [i];

					switch (gesture.Type) {
					case Gesture.GestureType.TYPE_CIRCLE:
						CircleGesture circle = new CircleGesture (gesture);

		                // Calculate clock direction using the angle between circle normal and pointable
						String clockwiseness;
						if (circle.Pointable.Direction.AngleTo (circle.Normal) <= Math.PI / 2) {
							//Clockwise if angle is less than 90 degrees
							clockwiseness = "clockwise";
						} else {
							clockwiseness = "counterclockwise";
						}

						float sweptAngle = 0;

		                // Calculate angle swept since last frame
						if (circle.State != Gesture.GestureState.STATE_START) {
							CircleGesture previousUpdate = new CircleGesture (controller.Frame (1).Gesture (circle.Id));
							sweptAngle = (circle.Progress - previousUpdate.Progress) * 360;
						}

						// SafeWriteLine ("  Circle id: " + circle.Id
		    //                            + ", " + circle.State
		    //                            + ", progress: " + circle.Progress
		    //                            + ", radius: " + circle.Radius
		    //                            + ", angle: " + sweptAngle
		    //                            + ", " + clockwiseness);
									// 				// send finger information to client
						SafeWriteLine(clockwiseness);
						String textinput;
						textinput = clockwiseness;
						NetworkStream serverStream = clientSocket.GetStream ();
						byte[] outStream = System.Text.Encoding.ASCII.GetBytes (textinput + "$");
						serverStream.Write (outStream, 0, outStream.Length);
						serverStream.Flush ();

						break;
						default:
							SafeWriteLine ("  Unknown gesture type.");
							break;
						}
					}
				}
			}
	}
	class Program
	{
		static void Main(string[] args)
		{
			SampleListener listener = new SampleListener();
			Controller controller = new Controller();
			controller.AddListener(listener);

			Console.WriteLine("Press Enter to quit...");
			Console.ReadLine();

			controller.RemoveListener(listener);
			controller.Dispose();
		}

	}
}