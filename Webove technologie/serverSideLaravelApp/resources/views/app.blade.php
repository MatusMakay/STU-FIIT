<!DOCTYPE html>
<html lang="en">
<head>
    @include('partials.header.head')
    
    <title>@yield('title')</title>
</head>
<body class="font-[Poppins] h-full no_scrollbar" >
    @include('partials.header.header')

    @yield('content')
    
    @include('partials.footer.footer')

    @include('partials.footer.footer-scripts')
</body>
</html>