namespace basic.aasync

module aasync =
    let add x y =
        async {
            return x+y
        }
    let compute x y =
        async {
            let! a = add x y
            let! b = add x y
            let! c = add x y
            return a+b+c
        }
