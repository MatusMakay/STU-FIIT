@extends('app')

@section('title')
    Domovská stránka
@endsection

@section('content')
    <section class="mt-20">
        <div class="flex items-center justify-center w-[100%] mx-auto">
            <img class="h-auto max-w-sm md:max-w-lg rounded-lg" src="../img/main/baby-yoda.png" alt="image description">
        </div>
    </section>

    <section class="mb-20">
        <h1 class="text-center text-6xl mb-4">Akcie</h1>
        <hr class="mb-5 w-[92%] mx-auto bg-gray-500 dark:bg-gray-700">
        <div class="flex-col md:flex md:flex-row items-center md:justify-evenly justify-center w-[75%] md:w-[92%] mx-auto mb-5">
            @foreach($actionProducts as $actionProduct)
            <div
                class="w-auto max-w-sm bg-white border border-gray-200 rounded-lg shadow dark:bg-gray-800 dark:border-gray-700">
                <a href="/products/{{$actionProduct->id}}">
                    <img class="p-8 rounded-t-lg" src="/img/main/{{$actionProduct->product_img}}" alt="product image" />
                </a>
                <div class="px-5 pb-5">
                    <a href="/products/{{$actionProduct->id}}">
                        <h5 class="text-xl font-semibold tracking-tight text-gray-900 dark:text-white">{{$actionProduct->item_name}}</h5>
                    </a>

                    <div class="flex items-center justify-between mt-3">
                        <span class="text-3xl font-bold text-gray-900 dark:text-white">{{$actionProduct->product_price}}$</span>
                        <a href="/products/{{$actionProduct->id}}"
                            class="text-white bg-gray-700 focus:ring-4 focus:outline-none focus:ring-gray-300 font-medium rounded-lg text-sm px-5 py-2.5 text-center bg-gray-600 hover:bg-orange-500 dark:focus:ring-gray-800 ">Do Košíka</a>
                    </div>
                </div>
            </div>
            @endforeach
        </div>
    </section>
@endsection
