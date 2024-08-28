->main



===main===
Hi. I'm Spitter. have you had lunch yet: 
    +[yes]
        Oh! Good.
         -> DONE
    +[no]
         oh!! 
        -> main2
        
    ->main2    
    
===main2====
let's go eat then. what re you in the mood for?
    +[kebab]
        -> chosen("cwalls")
    +[indian]
        -> chosen("choice 6")
    +[burger]
        -> chosen("choice 7")
    +[pizza]
        -> chosen("choice 8")
            
        
        
===chosen(choice)===

-> END