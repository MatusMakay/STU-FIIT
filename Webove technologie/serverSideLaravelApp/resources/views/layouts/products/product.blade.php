@extends('app')


@section('title')
    {{ $productType }}
@endsection

@section('content')
    <main class="mx-auto max-w-7xl px-4 sm:px-6 lg:px-8">
        <div class=" mt-12">
            <!-- Product info -->
            <div class="mx-auto max-w-2xl px-4 pt-10 pb-16 sm:px-6 lg:grid lg:max-w-7xl lg:grid-cols-4 lg:grid-rows-[auto,auto,1fr] lg:gap-x-8 lg:px-8 lg:pt-16 lg:pb-1">
                <div class="py-10 lg:col-span-2 lg:col-start-1 lg:border-r lg:border--200 lg:pt-6 lg:pb-16 lg:pr-8">
                <!-- Description and details -->
                    <div >
                        <h3 class="sr-only">Description</h3>

                        <div class="lg:grid lg:grid-cols-1 lg:gap-y-8 sm:grid sm:grid-cols-1 sm:gap-y-8"  >
                            <div class="static rounded-lg">
                                <img src="../img/products/{{ $productImg }}" alt="Model wearing plain black basic tee." class="h-full w-[100%] md:w-[80%] mx-auto ">
                            </div>
                        </div>
                    </div>
                </div>
                <!-- Options -->
                <div class="mt-4 lg:row-span-3 lg:col-span-2 lg:mt-0">

                    <h1 class="text-2xl font-bold tracking-tight text-gray-900 sm:text-3xl">
                        {{ $productName }}
                    </h1>

                    <h2 class="text-l font-bold tracking-tight mt-10 text-gray-900 sm:text-3xl">
                        Informácie o produkte
                    </h2>

                    <p class="text-base text-gray-900 mt-4">
                        {{ $productDescription }}
                    </p>
                    @if($count === 0)
                        <h2 class="text-l font-bold tracking-tight mt-10 text-gray-900 sm:text-3xl">
                           Nemame na sklade
                        </h2>
                    @else
                    <h2 class="text-l font-bold tracking-tight mt-10 text-gray-900 sm:text-3xl">
                        Pridane do kosika
                    </h2>
                    @endif
                </div>
                <form class="my-5 " action="{{route('add-to-cart', ['product' => $productId])}}" method="POST" >
                @csrf
                    <!-- Colors -->
                    @include('partials.product.color-picker')

                    <!-- Sizes -->
                    @include('partials.product.size-picker')
                    <div class="mt-4">
                        <h3 class="text-sm font-medium text-gray-900">Cena</h3>
                        <p class="text-3xl tracking-tight text-gray-900">{{$productPrice}}$</p>
                    </div>


                        <input value="{{$productId}}" name="productId" style="visibility: hidden"/>
                        <button type="submit" style="background: #343434;" class="mt-4 mb-14 flex w-full items-center justify-center rounded-md border bg-gray-400 py-3 px-8 text-base font-medium text-white hover:bg-orange-400 focus:outline-none focus:ring-2 focus:ring-indigo-500 focus:ring-offset-2">Pridať do košíka</button>

                </form>
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
        </div>
    </main>

@endsection
