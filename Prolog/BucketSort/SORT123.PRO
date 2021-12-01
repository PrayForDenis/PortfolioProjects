NOWARNINGS
CODE=1700

DOMAINS 
	n = integer
	digit = real
	list_of_real = digit*
	buckets = list_of_real*

PREDICATES 
	input_list(list_of_real)
	length(list_of_real, n)
	lengthCheck(list_of_real, n)
	separate_buckets(list_of_real, digit, n, buckets, buckets)
	sort_buckets(buckets, list_of_real, list_of_real)
	max(digit, digit, digit)
	min(digit, digit, digit)
	max_list(list_of_real, digit)
	min_list(list_of_real, digit)
	init_Buckets(buckets, buckets, n)
	insert(digit, n, buckets, buckets) 
	insert_in_list(digit, list_of_real, list_of_real)
	asc_order(digit, digit)
	sort_insert(list_of_real, list_of_real)
	insert_elem_for_sort(digit, list_of_real, list_of_real)
	merge_list(list_of_real, list_of_real, list_of_real)
	write_list(list_of_real)

GOAL
	write("Vvod lista: "), nl,
	input_list(List), nl,
	length(List, L),
	lengthCheck(List, L), nl,
	readchar(_).

CLAUSES 
	input_list([Head | Tail]) :-
		write("Vvedite chislo ili pystyu stroku dlya zaversheniya: "), readreal(Head), nl, !,
		input_list(Tail).
	input_list([]).
		 
	length([], 0).
	length([_ | Tail], L) :-
		length(Tail, P), L = P + 1.

	max(X, Y, X) :- X >= Y.
	max(X, Y, Y) :- X < Y.
	min(X, Y, X) :- X < Y.
	min(X, Y, Y) :- X >= Y.

	max_list([X], X).
	max_list([Head | Tail], M) :- 
		max_list(Tail, M_T), max(Head, M_T, M).

	min_list([X], X).
	min_list([Head | Tail], M) :- 
		min_list(Tail, M_T), min(Head, M_T, M).

	init_Buckets([H|T], EmptyBuck, 1) :- EmptyBuck=[[]|T], !.
	init_Buckets([H|T], EmptyBuck, N) :- N1 = N-1, H=[], EmptyBuck=[H|Res], init_Buckets(T, Res, N1).

	lengthCheck(List, L) :- 
		L = 0, write("Pustoi list: "), nl, !, fail;		
		L < 2, write("Vyvod lista: "), nl, write_list(List), !, fail;
		min_list(List, Min), max_list(List, Max), Min = Max, write("Vyvod lista: "), nl, write_list(List), !, fail;
		L >= 2, max_list(List, Max), init_Buckets(NullB, Buckets, L), separate_buckets(List, Max, L, Buckets, SepBuckets),
			sort_buckets(SepBuckets, Mass, SortMass), write("Otsortirovanniy spisok:"), nl, write_list(SortMass).

	separate_buckets([], _, _, Buckets, Buckets).
	separate_buckets([H|T], Max, L, Buckets, SepBuckets) :-
		Index = H * (L-1) / Max, Index < 0, Id = 0, insert(H, Id, Buckets, Res), separate_buckets(T, Max, L, Res, SepBuckets);
		Index = H * (L-1) / Max, Index >= 0, Id = round(Index), insert(H, Id, Buckets, Res), separate_buckets(T, Max, L, Res, SepBuckets).

	sort_buckets([], M, M).
    sort_buckets([P|T], L, R) :- 
	    sort_insert(P, ListS), 
		merge_list(L, ListS, Res),
	    sort_buckets(T, Res, R).
		
	insert(Elem, 0, [H|T], Final) :- insert_in_list(Elem, H, NewLine), Final = [NewLine|T], !.
	insert(Elem, N, [H|T], Final) :- N1=N-1, insert(Elem, N1, T, P), Final=[H|P].

	insert_in_list(Elem, Line, FinalLine) :- FinalLine=[Elem|Line].

	sort_insert([], []).
	sort_insert([X|Tail], Sorted_list) :- sort_insert(Tail, Sorted_Tail), insert_elem_for_sort(X, Sorted_Tail, Sorted_list). 
 
	insert_elem_for_sort(X, [Y|Sorted_list], [Y|Sorted_list1]) :- asc_order(X, Y), !, insert_elem_for_sort(X, Sorted_list, Sorted_list1).
	insert_elem_for_sort(X, Sorted_list, [X|Sorted_list]).
 
	asc_order(X, Y) :- X > Y.

	merge_list(L, [], L).
	merge_list([], L, L).
	merge_list([H|T], L, [H|M]) :- merge_list(T, L, M).

	write_list([Head | Tail]) :-
		write(Head), nl,
		write_list(Tail).
	write_list([]).