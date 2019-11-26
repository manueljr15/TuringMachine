using System;
using System.IO;
using System.Text;

namespace TuringMachine {
    /*
 * Turing.cs is used to create the states and transitions of the Turing Machine.
 * We have a class called Turing which will be the one we use to create the machine
 * It only has two variable, nodes and nStates.
 *
 * "nodes" will be a list of nodes to store all the states in the machine
 * "nStates" will be a variable to count the total number of states in the Turing Machine
 */
    class Turing {//Turing machine's creation. 

        private StateNode nodes = new StateNode(0);//List of states in the Turing Machine.
        public short nStates;//Number of states in the Turing Machine

        //Constructor of the Turing Machine
        public Turing(short addSub) {
            string stateFile;//Variable for the name of the file .txt that contains the number of states in the Turing Machine
            string transitionFile;//Variable for the name of the file .txt that contains the transitions for each state 

            //Variables for the files, either addition or subtraction
            if (addSub != 1) {
                stateFile = "StatesSub.txt";
                transitionFile = "TransitionsSub.txt";
            } else {
                stateFile = "StatesAdd.txt";
                transitionFile = "TransitionsAdd.txt";
            }

            //Read the 'states*' file to get the number or states in the Turing Machine
            //**********IMPORTANT: VERIFY THE LOCATION OF THE FILE AND REPLACE WITH THE FOLDER WHERE IT'S LOCATED************

            //Read each line and return an array of strings for each line in the file .txt
            string[] lines = System.IO.File.ReadAllLines(stateFile);

            //For each line in the array, the function createState is called to create a new state in the Turing Machine
            foreach (string linea in lines) {
                if (short.Parse(linea) != 100)//100 marks the end of the states in the Turing Machine
                    createState(short.Parse(linea));//Create a new state and adds it to the State's list
            }

            //Read the 'transitions*' file to get the transitions of each state in the Turing Machine
            //**********IMPORTANT: VERIFY THE LOCATION OF THE FILE AND REPLACE WITH THE FOLDER WHERE IT'S LOCATED************

            //Read each line and return an array of strings for each line in the file .txt
            string[] transitions = File.ReadAllLines(transitionFile);

            foreach (string trans in transitions) {
                //Split the line of text to get the values required to create the transition as follows:

                //Index on the array.   What the number, character or letter represents.
                //0.                    The current state
                //1.                    The next state
                //2.                    What it is on the tape
                //3.                    What it will put on the tape
                //4.                    Movement of the tape

                //Since the string that return is a string like "a,b,c,d,e" it need to be splited by the ',' (coma), this returns a char array
                var t = trans.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                //Function to create the transition, with the array that contains the requirements previously explained
                createTransitions(t);

            }


        }

        //Create a transition and associate it with the state's number
        private void createTransitions(string[] t) {

            StateNode sAux = null, sAux2 = null;//States nodes auxiliars to search if the current state and the destination state exists
            TransitionNode trans = null;//New transition node to create

            if (nodes != null) {//If nodes is not null, we can add transitions for those states

                //fetchNode returns a state node.
                sAux = fetchNode(short.Parse(t[0]));//Search for a initial state with the Id given

                //If the node returned is null, it doesn't exist in the Turing Machine
                if (sAux == null) Console.WriteLine("The starting node with the ID does not exist.");

                else {
                    //Return the destination node
                    sAux2 = fetchNode(short.Parse(t[1]));//Search for the destination state with the Id given

                    if (sAux2 == null) Console.WriteLine("The destination node with the ID does not exist.");

                    else {
                        trans = new TransitionNode(sAux2, t);//Create the transition node with the destination node of the current state
                        trans.setNextTNode(sAux.getTransition());//Create a list of transitions in the current state of sAux
                        sAux.setTransition(trans);//Associating the transition to the state
                    }
                }
            } else Console.WriteLine("There aren't states in the Turing Machine");

        }

        //Create the states for the Turing Machine
        private void createState(short state) {
            StateNode nodeAux = nodes;//Create an auxiliar node to determinate whether already exists a state in the Turing Machine

            if (nodeAux == null) {//If nodeAux is null create the first state in the machine
                nodes = new StateNode(state);//Create a new state with the Id given
                nStates++;//Number of states in the machine
            } else {

                //Search in the current states in the machine whether already exists a state with the same number or Id.
                nodeAux = fetchNode(state);//Returns an empty node where the new state will be stored
                if (nodeAux == null) {//If nodeAux is empty we use it to store the new state

                    nodeAux = new StateNode(state);//Create a new state node in the Turing Machine
                    nodeAux.nextSNode = nodes;//Add the node to the list in the Turing Machine
                    nodes = nodeAux;//New node is now the top of the list
                    nStates++;//Number of state in the machine

                } else Console.WriteLine("State already on the list");
            }
        }

        //Search for a state node with the Id given
        public StateNode fetchNode(short id) {
            StateNode nodeAux = nodes;//A copy of the List of state in the Turing Machine

            if (nodeAux != null) {//If nodeAux is not null, it means  we have states in the Turing Machine already stored

                while (nodeAux != null && nodeAux.getStateId() != id) {//Iterate in the list of states until neither the auxiliar node nor the id are equals
                    nodeAux = nodeAux.nextSNode;//Put the reference of the last known state in the auxiliar, meaning we have search through all the states
                }
            } else Console.WriteLine("Turing Machine state list is empty");

            return nodeAux;//Return the last empty node where the new will be stored
        }

        //Printing all the states and transitions in the Turing Machine
        public void printTuringMachine() {
            StateNode nodeList = nodes;//We have a list of states
            TransitionNode transitions = null;//We use this variable to get through all the transitions of one state

            if (nodeList != null) {//If nodeList it not null, we print them. Otherwise there aren't states to print

                Console.WriteLine("Number of states in the Turing Machine: {0}", nStates);//Printing the total number of states in the Turing Machine

                while (nodeList != null) {

                    Console.Write("{0} :[", nodeList.getStateId());//Printing the ID of the states
                    transitions = nodeList.getTransition();//Get the first transition of the current state

                    while (transitions != null) {//Each iteration is a transition
                        Console.Write("{0} ", transitions.getDestNode().getStateId());//Printing the destination node after the transition is completed
                        transitions = transitions.getNextTNode();//We pass to the next transition of the SAME state.
                    }
                    Console.WriteLine("]");
                    nodeList = nodeList.getNextSNode();//We pass to the next state
                }
            } else Console.WriteLine("There is nothing to print.");
        }


        //Encapsulation of variables
        public StateNode getNodes() { return nodes; }
        public short getNState() { return nStates; }
        public void setNodes(StateNode nodes) { this.nodes = nodes; }
        public void setNState(short nStates) { this.nStates = nStates; }
    }



    class Program {

        //Main function of the Project
        static void Main(string[] args) {

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
            string fileName = file + ".txt";
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
                        using (StreamWriter file =
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

                        //Write in the file the tape and the pointer
                        using (StreamWriter file =
                            new StreamWriter(fileName, true)) {
                            file.WriteLine(tape);
                            file.WriteLine(pointer);

                            file.WriteLine("Q: {0}\n" +
                                "Next Q: {1}\n" +
                                "Char in tape: {2}\tRep in tape: {3}\n" +
                                "Movement: {4}\n",
                                q0.getStateId(),
                                transition.getDestNode().getStateId(),
                                transition.getCharInTape(),
                                transition.getRepInTape(),
                                transition.getMovement());
                        }

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


    /*
 * Nodes.cs here we can see the definition of the nodes that we need to use in order to create the Turing Machine
 * 
 * We have two classes, StateNode and TransitionNode
 * StateNode needs a variable of the same type to make a reference of the next state, this creates a dynamic list
 * TransitionNode needs a variable of the same type as well to create a dynamic list of transitions
 * 
 */

    //State nodes
    /*
     A Turing Machine need a set of states through which we will determinate how is the tape at that moment or 'state'
     The states also help us to determinate whether the head of the tape is in a final or valid state
     A state can have 0 or n number of transitions. 
         */
    class StateNode {

        public StateNode nextSNode = null;//Next status node
        public TransitionNode transition = null;//Transition node
        private short stateId;//State's Id


        //Constructor of the class StateNode
        public StateNode(short stateId) {

            this.nextSNode = null;//Set the next node to null
            this.transition = null;//Set the transitions to null
            this.stateId = stateId;//Set the state's id
        }


        //Encapsulation of variables
        public short getStateId() { return this.stateId; }
        public StateNode getNextSNode() { return this.nextSNode; }
        public TransitionNode getTransition() { return this.transition; }

        public void setStateId(short stateId) { this.stateId = stateId; }
        public void setNextSNode(StateNode nextSNode) { this.nextSNode = nextSNode; }
        public void setTransition(TransitionNode transition) { this.transition = transition; }


    }

    //Transitions Nodes
    /*
     The transitions are the rules that will allow us to move through the states and the tape.
     We need to know in which state we are, what is the next state given a certain condition (a character in this case) in the tape and
     what will we do with the tape at that point, either override the tape, replace or put a space on it.
         */
    class TransitionNode {
        private TransitionNode nextTNode;//Next transition node 
        private StateNode destNode;//State destination node 
        private char charInTape;//Character in the tape
        private char repInTape;//Character to replace in tape
        private char movement;//Movement of the tape

        //Constructor of the TransitionNode
        //We need a StateNode type variable which will be the destination node to the next state in the Turing Machine
        //String array named t contains the conditions for the transition in order to move to the next state and/or finish the Turing Machine
        public TransitionNode(StateNode destinationNode, string[] t) {

            destNode = destinationNode;//State destination node

            //Index on the array.   What the number, character or letter represents.

            //2.                    What it is on the tape
            //3.                    What it will put on the tape
            //4.                    Movement of the tape
            charInTape = char.Parse(t[2]);
            repInTape = char.Parse(t[3]);
            movement = char.Parse(t[4]);

            //Reference to the next transition node, in case there is. This is asigned later
            nextTNode = null;
        }

        //Encapsulation of variables
        public char getCharInTape() { return this.charInTape; }
        public char getRepInTape() { return this.repInTape; }
        public char getMovement() { return this.movement; }
        public TransitionNode getNextTNode() { return this.nextTNode; }
        public StateNode getDestNode() { return this.destNode; }

        public void setCharInTape(char charInTape) { this.charInTape = charInTape; }
        public void setRepInTape(char repInTape) { this.repInTape = repInTape; }
        public void setMovement(char movement) { this.movement = movement; }
        public void setNextTNode(TransitionNode nextTNode) { this.nextTNode = nextTNode; }
        public void setNextTNode(StateNode destNode) { this.destNode = destNode; }

    }
}



