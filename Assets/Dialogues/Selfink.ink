->main



===main===
Hello. Welcome to our planet? (press space bar to continue.)
Remember; you should try to make your decisions based on the fact at hand, not your beliefs and background. Remember that and you'll surely do better than the last ones.
Now you have to make a choice.
one of these answers will lead to something better, one will lead to sometthing worse. choose your answer : 
    +[better]
        -> better
    +[same]
        -> same
    +[same]
        -> same
    +[worse]
        -> worse
       
===same===
the same, so the same it is. Bye.
->END
===better====
So you want things to get better, huh? we'll see..
now to wrap things up here, what do you say we solve a litle puzzle together? 
as you can see i have 3 legs. on special occasions I like to wear clorful socks to liven the place up. now, I have 4 red socks, 6 blue socks and 8 yellow ones? which is more likely to happen on a christmas day? me wearing 

    +[three socks of the same color]
        -> wrong("")
    +[three socks each with a different color]
          ->  wrong("")
    +[two socks the same and one in different color]
        Correct! you're a smart cookie aren't you?
-> END        
   
=== wrong(ch) ===            
        naaah!  two socks the same and one in different color is more likely.
-> END        
===chosen(choice)===
{choice}
-> END
===worse===
you're really wise you know that? yes! to get better it always gets worse at first. I don't think there's anything left for me to say. Bye.
->END


