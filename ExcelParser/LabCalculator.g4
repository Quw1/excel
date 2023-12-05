grammar LabCalculator;
/*
 * Parser Rules
 */
compileUnit : expression EOF;

expression :
	LPAREN expression RPAREN #ParenthesizedExpr
	| operatorToken=(ADD | SUBTRACT) expression #UnaryExpr
	| expression EXPONENT expression #ExponentialExpr
    	| expression operatorToken=(MULTIPLY | DIVIDE) expression #MultiplicativeExpr
	| expression operatorToken=(ADD | SUBTRACT) expression #AdditiveExpr
	| MMAX LPAREN paramlist=arglist RPAREN #MmaxExpr
	| MMIN LPAREN paramlist=arglist RPAREN #MminExpr
	| MAX LPAREN expression ',' expression RPAREN #MaxExpr
	| MIN LPAREN expression ',' expression RPAREN #MinExpr
	| expression operatorToken=(GT | LT) expression #GLTExpr
	| expression operatorToken=(GE | LE) expression #GLEExpr
	| expression operatorToken=(EQ | NEQ) expression #ENQExpr
	
	| NUMBER #NumberExpr
	| IDENTIFIER #IdentifierExpr
	; 
	arglist: expression (',' expression)*;
	//paramlist: expression (',' expression)+;

/*
 * Lexer Rules
 */
NUMBER : INT ('.' INT)?; 
IDENTIFIER : [a-zA-Z]+[1-9][0-9]*;

INT : ('0'..'9')+;

EXPONENT : '^';
MULTIPLY : '*';
DIVIDE : '/';
SUBTRACT : '-';
ADD : '+';
LPAREN : '(';
RPAREN : ')';
MMAX : 'mmax';
MMIN : 'mmin';
MAX : 'max';
MIN : 'min';

GT: '>';
LT: '<';
GE: '>=';
LE: '<=';
EQ: '=';
NEQ: '<>';

WS : [ \t\r\n] -> channel(HIDDEN);