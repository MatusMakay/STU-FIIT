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

<body class="font-[Poppins] bg-white min-h-screen no_scrollbar ">
    <main class="flex items-center justify-center h-full bg-gray-100 overflow-hidden ">
    <form method="POST" action="{{ route('login') }}">
        @csrf
            <div class="bg-white w-[90%] p-6 rounded shadow-md mx-auto">
                <div class="flex items-center justify-center mb-4">
                    <img src="img/baby-yoda.png" alt="" class="h-32">
                </div>
                <label for="email" class="text-gray-700">Email</label>
                <input type="text" placeholder="Email"
                    class="w-full py-2 bg-gray-100 text-gray-500 px-1 outline-none mb-4" required name="email" id="email" required autofocus autocomplete="username" >

                <label for="password" class="text-gray-700 mt-5">Heslo</label>
                <input type="password" placeholder="Heslo"
                    class="w-full py-2 bg-gray-100 text-gray-500 px-1 outline-none mb-4" name="password" required autocomplete="current-password"  id="password">

                <div class="flex justify-between items-center px-8 py-8">
                    <button style="background: #343434" class="bg-gray-600 text-1xl hover:text-orange-400 rounded text-white" type="submit">Prihlasenie</button>
                    <button class="bg-gray-600 text-1xl hover:text-orange-400 rounded text-white"><a href="{{url('/registration')}}">Registracia</a></button>
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
            </div>
        </form>
    </main>
</body>
