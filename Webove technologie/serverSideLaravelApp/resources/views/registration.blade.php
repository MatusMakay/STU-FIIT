<html>

<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    @vite(['resources/css/app.css', 'resources/js/app.js'])
    <script src="https://cdn.tailwindcss.com"></script>
    <link href="https://fonts.googleapis.com/css2?family=Poppins:wght@300&display=swap" rel="stylesheet">
    <title>Document</title>
</head>

<body class="font-[Poppins] bg-white h-full no_scrollbar ">
    <main class="min-h-screen py-14">
        <dir class="container mx-auto pl-0">
            <div class="flex md:w-8/12 w-full rounded-x1 mx-auto shadow-lg overflow-hidden ">
                <div
                    class="w-1/2 register-image  text-white flex flex-col justify-center items-center py-12 px-12 bg-no-repeat bg-cover  md:block">
                    <h1 class="text-3xl mb-3 text-center ">Welcome</h1>
                    <div class="text-center">
                        <p>orem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has
                            been the industry's standard dummy text ever since the 1500s, when an unknown printer took a
                            galley of type and scrambled it to make a type specimen book. </p>
                    </div>
                </div>
                <div class="w-full md:w-1/2 md:py-16 md:px-12 py-8 px-4 border border-gray-400">
                    <h1 class="text-3xl mb-4">Register</h1>
                    <p>Vytvor si ucet</p>

                    <form  method="POST" action="{{ route('registration') }}" class="mt-4">
                        @csrf
                        <div>
                            <input type="text" placeholder="Meno" class="border border-gray-300 py-1 px-2 w-full" name="name" id="name" required autofocus autocomplete="name" >
                        </div>
                        <div class="mt-5">
                            <input type="text" placeholder="Email" class="border border-gray-300 py-1 px-2 w-full" name="email" id="email" required autocomplete="username">
                        </div>
                        <div class="mt-5">
                            <input type="password" placeholder="Heslo" class="border border-gray-300 py-1 px-2 w-full" name="password" id="password" required autocomplete="new-password">
                            <input type="password" placeholder="Zopakuj heslo" class="border border-gray-300 py-1 px-2 w-full mt-5"  name="password_confirmation" id="password_confirmation" required autocomplete="new-password" />
                        </div>
                        <div class="mt-5">
                            <input type="Text" placeholder="Krajina" class="border border-gray-300 py-1 px-2 w-full">
                        </div>
                        <div class="mt-5">
                            <input type="checkbox" name="" id="" class="border border-gray-400">
                            <span>Prijimam podmienky pouzitia</span>
                        </div>
                        <div class="mt-5">
                            <button class="w-full bg-gray-600 hover:bg-orange-400 py-3 text-white" type="submit"><a>Registuj ma</a></button>
                        </div>
                        @if($errors->all())
                        <div class="alert alert-danger">
                            <ul>
                                @foreach($errors->all() as $error)
                                <li>{{$error}}</li>
                                @endforeach
                            </ul>
                        </div>
                        @endif
                    </form>
                </div>
            </div>
        </dir>
    </main>

</body>

</html>
