/*
 * Nodes.cs here we can see the definition of the nodes that we need to use in order to create the Turing Machine
 * 
 * We have two classes, StateNode and TransitionNode
 * StateNode needs a variable of the same type to make a reference of the next state, this creates a dynamic list
 * TransitionNode needs a variable of the same type as well to create a dynamic list of transitions
 * 
 */

namespace TuringMachine {
    
    //State nodes
    /*
     A Turing Machine need a set of states through which we will determinate how is the tape at that moment or 'state'
     The states also help us to determinate whether the head of the tape is in a final or valid state
     A state can have 0 or n number of transitions. 
         */
    class StateNode{

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
    class TransitionNode
    {
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
