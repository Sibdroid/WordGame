# WordGame

A vocabulary-based game slightly inspired by Spelling Bee. 
The rules are as follows:
1. You start with two random four-letter words.
2. The words have unique letters: you get a total of eight.
3. You score points by finding words that have letters from each word.
5. The minimum length of the word is four letters.
7. You always get at least as many points as the amount of letters in the word.
8. Longer and more interesting words award more points.
    1. Words with 5 to 7 letters award 1 additional point.
    2. Words with 8 to 10 letters award 2 additional points.
    3. Words with 11 or more letters award 3 additional points.
    4. Words that use all letters in both words award double points.
    5. The double points are counted after the additional points.

Examples: 
1. 'near' and 'gist' is given.
    1. 'agents' awards 7 (6+1) points.
    2. 'gannister' awards 11 (9+2) points.
    3. 'arresting' awards 22 ((9+2)*2) points. 
