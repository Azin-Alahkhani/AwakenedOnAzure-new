->main



===main===
Hi there. I'm awli. I see you've already my friend Leno. he is a grumpy one isn't he? : 
    +[yes, he was wierd]
        yes, he was wierd
        ->weird
    +[I thought he was alright]
        I thought he was alright
        ->alright
    +[depends on your defenition of grumpy]
       depends on your defenition of grumpy
        -> main2
        
  
    
===main2====
I agree. anyways, that's not what I wanted to talk to you about. you have to make another choice.
    +[1]
        -> chosen("choice 1")
    +[2]
        -> chosen("choice 2")
    +[3]
        -> chosen("choice 3")
    +[4]
        -> chosen("choice 4")
            
        
===chosen(choice)===
 {choice}
-> END

===weird===
well you shoud see the rest of us. haha.

->END

===alright===
hmmm.

->END