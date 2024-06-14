predicates
  nondeterm femme(symbol).
  nondeterm homme(symbol).
  nondeterm progeneteur(symbol , symbol).
  nondeterm pere(symbol, symbol).
  nondeterm grandpere(symbol ,symbol).
  nondeterm grandmere(symbol , symbol).
  nondeterm frere(symbol,symbol).
  nondeterm ante(symbol , symbol).
  nondeterm oncle(symbol , symbol).
  nondeterm cousin(symbol , symbol).
  
   
clauses 
femme(fatima).
homme(ahmed).
progeneteur(fatima,ali).
progeneteur(ahmed,ali).
progeneteur(ahmed,salma).
pere(X,Y):- homme(X), progeneteur(X,Y).
grandpere(X,Y):-homme(X), progeneteur(X,V) , progeneteur(V,Y).
grandmere(X,Y):-femme(X), progeneteur(X,V) , progeneteur(V,Y).
frere(X,Y):-homme(X), progeneteur(V,X) , progeneteur(V,Y).
oncle(X,Y):-homme(X) , frere(X,V) , progeneteur(V,Y).
ante(X,Y):-femme(X),frere(X,V) , progeneteur(V,Y).
cousin(X,Y):-progeneteur(V,X) , oncle(V,Y) ; ante(V,Y).


goal

femme(Fatima).
frere(salma,ali).








   