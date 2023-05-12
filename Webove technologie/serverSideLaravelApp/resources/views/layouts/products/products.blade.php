@extends('app')

@section('title')
    {{ $productName }}
@endsection

@section('content')

    <main class="mx-auto max-w-7xl px-4 sm:px-6 lg:px-8">
        <div class="flex  md:items-baseline justify-between border-b border-gray-200 pt-24 pb-6">

            <button
             id="filter-mobile" onclick="filterClick()" class=" sm:block md:hidden w">
                <img src="img/icons/add-circle-outline.svg" class="w-10 h-10" alt="">
            </button>

            <div class="text-center w-full ">
                <h1 class="md:ml-20 text-4xl font-bold tracking-tight text-gray-900 ">{{$category}}</h1>
            </div>
            @include('partials.filters.popup-filters')
        </div>

        <section class="pt-6 pb-24">
            <div class="md:grid gap-x-8 gap-y-10 md:grid-cols-4">
                <!-- Filters -->
                <div id="filters"
                    class="md:block sm:hidden max-sm:w-[60%] max-sm:absolute max-sm:bg-white max-sm:p-6 max-sm:rounded-lg max-sm:border max-sm:border-gray-600 max-sm:mt-[-35px] max-sm:z-1">
                    <form class="block w-80px columns-1xs" action="/filter">
                        <input name="category" value="{{$category}}" class="hidden"/>
                        <input name="type" value="{{$type}}" class="hidden"/>

                        @include('partials.filters.main-filters')
                </div>

                <div class="md:col-span-3 sm:col-span-2 ">
                    @if($products->isEmpty())
                        @include('partials.product.no-products')
                    @else
                        <div
                            class="mt-6 grid grid-cols-1 gap-y-10 md:grid-cols-2 gap-x-6 sm:grid-cols-1 lg:grid-cols-3 xl:grid-cols-3">
                            @foreach($products as $product)
                                <div class=" sm:max-w-sm lg:h-md ">
                                    <a href="/products/{{$product->id}}" > 
                                        <div class="min-h-80 w-full overflow-hidden rounded-md ">
                                            <img src="img/products/{{$product->product_img}}"
                                                alt="Front of men&#039;s Basic Tee in black."
                                                class="h-full w-full object-cover object-center lg:h-full lg:w-full">
                                        </div>
                                    </a>
                                    <div class="mt-4 flex justify-between">
                                        <div>
                                            <h3 class="text-m text-gray-700">
                                                <a href="/products/{{$product->id}}">
                                                    {{$product->item_name}}
                                                </a>
                                            </h3>
                                            <hr class="mb-2">
                                            <p class="mt-1 text-sm text-gray-500"><span class="text-gray-700">Značka: </span> {{ $product->brand }}</p>
                                            <p class="mt-1 text-sm text-gray-500"><span class="text-gray-700">Kategória: </span> {{ $product->category }}</p>
                                            <p class="mt-1 text-sm text-gray-500"><span class="text-gray-700">Farba: </span> {{ $product->color }}</p>
                                        </div>
                                        <p class="text-m font-medium text-gray-900">{{$product->product_price}}$</p>
                                    </div>
                                </div>
                            @endforeach
                        </div>
                    @endif
                </div>
            </div>
        </section>
    </main>
@endsection