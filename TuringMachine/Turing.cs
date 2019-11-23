/*
 * Turing.cs is used to create the states and transitions of the Turing Machine.
 * We have a class called Turing which will be the one we use to create the machine
 * It only has two variable, nodes and nStates.
 *
 * "nodes" will be a list of nodes to store all the states in the machine
 * "nStates" will be a variable to count the total number of states in the Turing Machine
 */
using System;

namespace TuringMachine {
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
            string[] lines = System.IO.File.ReadAllLines(@"C:\<DIRECTORY>\"+stateFile);

            //For each line in the array, the function createState is called to create a new state in the Turing Machine
            foreach (string linea in lines) {
                if (short.Parse(linea) != 100)//100 marks the end of the states in the Turing Machine
                    createState(short.Parse(linea));//Create a new state and adds it to the State's list
            }

            //Read the 'transitions*' file to get the transitions of each state in the Turing Machine
            //**********IMPORTANT: VERIFY THE LOCATION OF THE FILE AND REPLACE WITH THE FOLDER WHERE IT'S LOCATED************

            //Read each line and return an array of strings for each line in the file .txt
            string[] transitions = System.IO.File.ReadAllLines(@"C:\<DIRECTORY>\"+transitionFile);

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
                if (sAux == null)Console.WriteLine("The starting node with the ID does not exist.");

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
}
