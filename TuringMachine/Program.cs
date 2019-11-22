/*
 TURING MACHINE
 Academic project for the Polytechnic University Of San Luis Potosí
 November 2019
 Information Technologies Engineering
 
 
 */


using System;
using System.IO;
using System.Text;

namespace TuringMachine
{   
    class Program{

        //Main function of the Project
        static void Main(string[] args){

            while (true) {
                Console.Clear();
                try {//Try-catch block to validate the correct format of entries 

                    Console.WriteLine("What would you like to do in the Turing Machine?\n1.Addition\n2.Subtraction");
                    short addSub = short.Parse(Console.ReadLine());

                    if (addSub == 1 || addSub == 2) {

                        Turing machine = new Turing(addSub);//Initialization of Turing Machine

                        while (true) {
                            Console.Clear();
                            try {//Try-catch to validate if the entries are in the valid options

                                Console.WriteLine("Turing Machine created\n1.Print Turing Machine.\n2.Evaluate a string.");
                                short op = short.Parse(Console.ReadLine());

                                if (op == 1) {
                                    machine.printTuringMachine();//Printing the Turing Machine
                                    introduceString(machine);//Function call to introduce a string to validate

                                } else if (op == 2) introduceString(machine);
                                else throw new Exception();//Throw exception if the input aren't neither 1 or 2

                            } catch (Exception e) {//Catch if the entry isn't a valid option
                                Console.WriteLine("Introduce a valid option.\nPress any key to continue.");
                                Console.ReadKey();

                            }
                        }


                    } else throw new Exception();//Throw exception if the input aren't neither 1 or 2

                } catch (FormatException e) {//Catch if the entry isn't a number
                    Console.WriteLine("Introduce a valid format.\nPress any key to continue.");
                    Console.ReadKey();
                } catch (Exception e) {//Catch if the entry isn't a valid option
                    Console.WriteLine("Introduce a valid option.\nPress any key to continue.");
                    Console.ReadKey();

                }

            }
            
            
        }

        //Funcion to validate the string
        private static void introduceString(Turing machine) {

            Console.WriteLine("Introduce the string to evaluate: ");
            string tape = Console.ReadLine();
               
            //In some cases, the Turing Machine needs to check one position after the last character in the string,
            //it is necessary to add a space, otherwise it would throw an IndexOutOfBoundException if there is null 
            tape += ' ';
            string pointer = "^";//Head pointer in the machine. Helps to visualize where we are at any moment on the tape
            
            //Create a new file where it will print each step and movement in the machine
            Console.WriteLine("Name of the file where the movement will be printed: ");
            string file = Console.ReadLine();

            //**********IMPORTANT: VERIFY THE LOCATION OF THE FILE AND REPLACE WITH THE FOLDER WHERE IT WILL BE LOCATED************
            string fileName = @"E:\UPSLP\7mo semestre\Teoría computacional\TuringMachine\TuringMachine\" + file + ".txt";
            using (FileStream fs = File.Create(fileName)) {//Creating a new file using FileStream
                Byte[] title = new UTF8Encoding(true).GetBytes("Printing tape\n");//Printing the head of the new file
                fs.Write(title, 0, title.Length);
            }

            //Recursive function to validate the string
            //Parameters: the first state in the Turing Machine, the string to evaluate, the position in the string and the file name to write on it each state.
            validateTuringMachine(machine.fetchNode(1), tape, 0, pointer, fileName);

        }

        //Recursive function to validate the string.
        //Here we use the transitions and we move throughout all the tape.
        //Disclaimer: after this, only god knows how it works. /s
        private static void validateTuringMachine(StateNode q0, string tape, short movement, string pointer, string fileName) {
            Console.Clear();

            TransitionNode transition = q0.getTransition();//Getting the first transition of the current node

            //Write in the file the tape and the pointer
            using (StreamWriter file =
            new StreamWriter(fileName, true)) {
                file.WriteLine(tape);
                file.WriteLine(pointer + "\n");
            }

            //Write in console the tape and the pointer
            Console.WriteLine(tape);
            Console.WriteLine(pointer);

            //Sleep function 
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(.5));

            //If the first transition of the current node is null, it only means that either we came to a final state or there isn't transition on that node.
            if (transition != null) {
                while (transition != null) {

                    // Add spaces at the end of the tape where the new characters will be written, as well as the pointer that should point them out.
                    if (tape.Length == movement) tape += ' ';
                    if (pointer.Length == movement) pointer += ' ';

                    //Try-catch block to determinate whether the index of the string called "movement" is lower than 0.
                    //Applies only in the subtraction.
                    try {
                        if (movement < 0) throw new Exception();
                    } catch (Exception E) {//Catch an exception when movement is lower than 0

                        //When this happends in the subtraction it means that either the result is 0 or a negative number,
                        //which is represented with the number of dashes left in the tape
                        using(StreamWriter file =
                            new StreamWriter(fileName, true)) {
                            file.WriteLine("Done");
                        }

                        //Since the operation is finished we exit the application
                        Console.WriteLine("Done\nPress any key to continue.");
                        Console.ReadKey();
                        Environment.Exit(1);
                    }

                    //Compare the current character in the tape on all the transitions of the state
                    //Tape is a string that contains the operation we introduce, movement is the index of that string
                    //We look in the transitions if the character is equal to what we have in the string in determinated index
                    if (tape[movement] == transition.getCharInTape()) {

                        //Removing the current character in the tape and use the "aux" variable to store the string without the character removed
                        string aux = tape.Remove(movement, 1);

                        //Using the "aux" variable, get the character to replace from the transition node and put them in the current index determinated for "movement"
                        aux = aux.Insert(movement, transition.getRepInTape().ToString());

                        //Same with the head pointer, but instead of getting the character from the transition node, use a space to replace it
                        string auxP = pointer.Replace("^", " ");
                        auxP = auxP.Insert(movement, "^");

                        //Conditions for the tape to know where to move next
                        //Determinate where will the tape will move next. 
                        //R == right. So increase "movement" variable to move to the right in the string
                        //L == left. Decrease "movement" to move to the left in the string
                        if (transition.getMovement() == 'R') movement++;
                        if (transition.getMovement() == 'L') movement--;

                        //Recursive call function, this time with
                        //destination state, 
                        //the new tape modified, 
                        //movement increased or decreased,
                        //head pointer modified and
                        //name file to keep writing on it
                        validateTuringMachine(transition.getDestNode(), aux, movement, auxP, fileName);

                    } else transition = transition.getNextTNode();//If the current character isn't in the current transition, move to next transition of the same state
                        
                    
                }
            } else {//There aren't transitions left, so the operation must been finished
                using (StreamWriter file =
                    new StreamWriter(fileName, true)) {
                    file.WriteLine("Done");
                }

                //Since the operation is finished we exit the application
                Console.WriteLine("Done\nPress any key to continue.");
                Console.ReadKey();
                Environment.Exit(1);
            }
        }

    }
}
