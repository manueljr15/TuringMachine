using System;
using System.Collections.Generic;
using System.Text;

namespace TuringMachine {
    class ValidateString {
        /*public ValidateString(StateNode q0, string tape, short movement) {
            TransitionNode transition = q0.getTransition();
            char[] tapeChar = tape.ToCharArray();
            if (transition!= null) {
                while (transition != null) {
                    Console.WriteLine(tapeChar);
                    Console.WriteLine("q: {0}\t{1}\t{2}", q0.getStateId(), transition.getCharInTape(), tapeChar[movement]);
                    Console.ReadKey();
                    if (tapeChar[movement] != transition.getCharInTape())
                        transition = transition.getNextTNode();
                    else {
                        tapeChar[movement] = transition.getRepInTape();
                        if (transition.getMovement().Equals("R"))
                            movement++;
                        else if (transition.getMovement().Equals("L"))
                            movement--;
                        else if (transition.getMovement().Equals("S"))
                            continue;
                        
                    }
                }
            }
        }*/

        /*ublic void validateTuringMachine(StateNode q0, char[] tape, short movement) {
            TransitionNode transition = q0.getTransition();
            if (transition != null) {
                while (transition != null) {
                    Console.WriteLine(tape);
                    Console.WriteLine("q: {0}\t{1}\t{2}", q0.getStateId(), transition.getCharInTape(), tape[movement]);
                    Console.ReadKey();
                    
                    if (tape[movement] != transition.getCharInTape())
                        transition = transition.getNextTNode();
                    else {
                        tape[movement] = transition.getRepInTape();
                        if (transition.getMovement().Equals("R"))
                            movement++;
                        else if (transition.getMovement().Equals("L"))
                            movement--;
                        else if (transition.getMovement().Equals("S"))
                            continue;
                        validateTuringMachine(transition.getDestNode(), tape, movement);
                    }
                }
            }
        }*/
    }
}
