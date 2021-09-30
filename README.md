# MiPrimerCompilador
## Gramática Original:
E	     ->    E+T   |    E-T   | left_T 
left_T	-> left_T*S | left_T/S | left_S
T	     ->    T*S   |    T/S   |    S
left_S ->   -F     |    S
S	     ->  (-F)    |    F
F	     ->  (E)     |    num 
##Gramática Final (Sin recursividad por la izquierda, factorizada)
E	     ->  left_TE' 
E'     ->    +TE'  |    -TE'  |   e
left_T	-> left_Sleft_T'
left_T'-> *Sleft_T'| /Sleft_T'|   e
T	     ->    ST'
T'	    ->   *ST'   |    /ST'  |   e
left_S ->   -F     |    S
S	     ->  (S'     |    num
S'     ->  -F)     |    E)
F	     ->  (E)     |    num 
