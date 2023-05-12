@extends('app')

@section('title')
    Košík
@endsection

@section('content')
<section class=" mt-20 md:mt-40 max-h-50">
        <h1 class="text-center py-6 text-2xl">Nákupný košík</h1>

        <div class="stattic overflow-x-hidden  sm:rounded-lg md:ml-10">
            <table class=" md:w-[80%] w-full  mx-auto text-sm text-left text-gray-500 dark:text-gray-400 overflow-y-hidden">
                <thead class="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400">
                    <tr>
                        <th scope="col" class="px-6 py-3">
                            Meno Produktu
                        </th>
                        <th scope="col" class="px-6 py-3 ">
                            Vymaž
                        </th>
                        <th scope="col" class="px-6 py-3">
                            Počet Kusov
                        </th>
                        <th scope="col" class="px-6 py-3 invisible md:visible">
                            Cena
                        </th>

                    </tr>
                </thead>
                <tbody>
                    @if($products != null)
                    @foreach($products as $product)
                        <tr class="bg-white dark:bg-gray-800 dark:border-gray-700">
                            <td scope="row" class="px-6 py-4 font-medium text-gray-900 whitespace-nowrap dark:text-white">
                                <span class="flex items-center justify-start">
                                    <img src="img/products/{{$product->id}}" alt="" class="h-8">
                                    <p class="ml-8">{{ $product->item_name }}</p>
                                </span>
                            </td>
                            <td class="px-6 py-4  md:visible">
                                <form method="POST" action="{{route('remove-from-cart', ['id' => $product->id])}}">
                                    @csrf
                                    <input value="{{$product->id}}" name="id" style="visibility: hidden"/>
                                    <button submit class="remove-cart-btn font-medium text-gray-600 dark:text-gray-500 hover:underline">Odstráň</button>
                                </form>
                            </td>
                            <td class="px-6 py-4">
                                <div class="flex">
                                    {{$product->product_price}}$
                                    <ion-icon name="close-outline" class="mt-1 ml-1 invisible md:visible"></ion-icon>
                                </div>
                            </td>

                        </tr>
                    @endforeach
                    @endif
                </tbody>
            </table>
        </div>

        <div class=" w-[90%] flex justify-center ml-5 md:ml-0 md:justify-end items-center gap-6 mt-4 md:mt-0">
            <form method="POST" action="{{route('checkContinue')}}">
            @csrf
            <button class="text-white rounded-md br w-24 h-12 bg-gray-600 hover:bg-orange-400" type="submit"> Zaplatiť</button>
            </form>
            <button class="text-white rounded-md br w-24 h-12 bg-gray-600 hover:bg-orange-400"><a href="/">Domov</a></button>

        </div>
    </section>
    <script src="../js/cart/cart.js">
@endsection
