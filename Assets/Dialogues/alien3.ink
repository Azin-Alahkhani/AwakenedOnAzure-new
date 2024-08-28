->main



===main===
Hi. I'm pompo. How was your journey so far: 
    +[Good, thanks]
        Awesome. let me know  if you help with need anything
        ->help
    +[not so great]
        I'm sorry to hear that. I hope you have better luck next time.
        ->DONE
    +[boring]
        Oh! thats rude.
        ->DONE
    +[interesting]
         Nice!! 
        -> main2
        
    ->main2    
    
===main2====
what interested you most : 
    +[the ruines on the walls]
        -> chosen("cwalls")
    +[the plants and nature]
        -> chosen("choice 6")
    +[you guys :)]
        -> chosen("choice 7")
    +[i like the song]
        -> chosen("choice 8")
            
        
        
===chosen(choice)===
oh thats nice :)
-> END

===bh===
oh i'm sorry i cant help with that.
->END

===help===
okay, how may i help you?
    +[give me a map of the planet]
        -> bh
    +[tell me where my ship is]
        -> bh
    +[where can i find food]
        -> bh
    +[what's this song?]
        -> bh

->END