@extends('app')

@section('title')
    Admin zóna
@endsection



@section('content')
    <section class=" mt-20 md:mt-40 max-h-50">
        <h1 class="text-center py-6 text-2xl">Admin</h1>

        <div class="stattic overflow-x-hidden  sm:rounded-lg md:ml-10">
            <table class=" md:w-[80%] w-full  mx-auto text-sm text-left text-gray-500 dark:text-gray-400 overflow-y-hidden">
                <thead class="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400">
                    <tr>
                        <th scope="col" class="px-6 py-3">
                            Meno Produktu
                        </th>
                    </tr>
                </thead>
                <tbody>
                    @php
                        $counter = 4;
                    @endphp
                @foreach($products as $product)
                        <tr class="bg-white dark:bg-gray-800 dark:border-gray-700">
                            <td scope="row" class="px-6 py-4 font-medium text-gray-900 whitespace-nowrap dark:text-white">
                                <span class="flex items-center justify-start">
                                    <img src="img/products/{{$product->id}}" alt="" class="h-8">
                                    <p class="ml-8">{{ $product->item_name }}</p>
                                </span>
                            </td>
                            
                            <td class="count-products px-6 py-4 text-right">
                                <button id="dropdownDefaultButton{{$counter}}" data-dropdown-toggle="dropdown{{$counter}}" data-trigger="click"
                                    type="button" style="background: gray" class="modal=open text-white rounded-md br w-24 h-8 bg-gray-600 hover:bg-orange-400">Edituj</button>
                                <div id="dropdown{{$counter}}"
                                    class="flex justify-center pt-4 bg-opacity-100 hidden bg-white  divide-gray-100 rounded-lg shadow dark:bg-gray-700">
                                    
                                    <div class="bg-white rounded-lg shadow relative dark:bg-gray-700">
                                        
                                        <form class="space-y-6 px-6 lg:px-8 pb-4 sm:pb-6 xl:pb-8" method="POST" action="/admin/{{$product->id}}">
                                        <input type="hidden" name="_method" value="PUT">
                                    
                                            {{ csrf_field() }}
                                            <div>
                                                <span>
                                                    <label for="name" class="text-sm font-medium text-gray-900 block mb-1 dark:text-gray-300">Názov produktu</label>
                                                    <input type="Text" name="name" id="name" value="{{ $product->item_name }}" class="bg-gray-50 border border-gray-300 text-gray-900 sm:text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-600 dark:border-gray-500 dark:placeholder-gray-400 dark:text-white" placeholder="Názov" required="">
                                                </span>
                                            </div>
                                            <div>
                                                <input name="productId" value="{{$product->id}}" class="hidden">    
                                                <label for="description" class="text-sm font-medium text-gray-900 block mb-1 dark:text-gray-300">Opis produktu</label>
                                                <input type="Text" name="productDescrition" id="description" value="{{ $product->description }}" placeholder="Opis" class="bg-gray-50 border border-gray-300 text-gray-900 sm:text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-600 dark:border-gray-500 dark:placeholder-gray-400 dark:text-white" required="">
                                            </div>
                                            <label for="color" class="text-sm font-medium text-gray-900 block mb-1 dark:text-gray-300">Farba Produktu</label>
                                            <input type="Text" name="productColor" id="color" value="{{ $product->color }}" placeholder="White, Pink, Black" class="bg-gray-50 border border-gray-300 text-gray-900 sm:text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-600 dark:border-gray-500 dark:placeholder-gray-400 dark:text-white" required="">

                                            <label for="productCount" class="text-sm font-medium text-gray-900 block mb-1 dark:text-gray-300">Počet na sklade</label>
                                            <input type="Text" name="productCount" id="productCount" value="{{ $product->number_products }}" placeholder="Počet na sklade" class="bg-gray-50 border border-gray-300 text-gray-900 sm:text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-600 dark:border-gray-500 dark:placeholder-gray-400 dark:text-white" required="">

                                            <label for="productPrice" class="text-sm font-medium text-gray-900 block mb-1 dark:text-gray-300">Cena za kus</label>
                                            <input type="Text" name="productPrice" id="productPrice" value="{{ $product->product_price }}" placeholder="Cena" class="bg-gray-50 border border-gray-300 text-gray-900 sm:text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-600 dark:border-gray-500 dark:placeholder-gray-400 dark:text-white" required="">

                                            
                                            <button style="background: gray" type="submit" class="text-white rounded-md br w-24 h-12 bg-gray-600 hover:bg-orange-400"> Uložiť</input>
                                        </form>
                                    </div>
                                </div>
                            </td>
                            <td class="px-6 py-4 invisible md:visible">
                                <form action="/admin/{{$product->id}}" method="POST">
                                    <input type="hidden" name="_method" value="DELETE">
                                    {{ csrf_field() }}
                                    <button style="background: gray" type="submit" class="text-white rounded-md br w-24 h-8 bg-gray-600 hover:bg-orange-400">Odstráň</button>
                                </form>
                            </td>
                        </tr>
                        @php
                            $counter++;
                        @endphp
                    @endforeach
                </tbody>
            </table>
        </div>
        <div id="dropdownCreate"
            class="relative flex justify-center pt-4 bg-opacity-100 hidden bg-white  divide-gray-100 rounded-lg shadow dark:bg-gray-700 top-[20%]">
            
            <div class="bg-white rounded-lg shadow relative dark:bg-gray-700">
                
                <form class="space-y-6 px-6 lg:px-8 pb-4 sm:pb-6 xl:pb-8" method="POST" action="/admin">
                    {{ csrf_field() }}
                    <div>
                        <span>
                            <label for="name" class="text-sm font-medium text-gray-900 block mb-1 dark:text-gray-300">Názov produktu</label>
                            <input type="Text" name="name" id="name" value="" class="bg-gray-50 border border-gray-300 text-gray-900 sm:text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-600 dark:border-gray-500 dark:placeholder-gray-400 dark:text-white" placeholder="Názov" required="">
                        </span>
                    </div>
                    <div>
                        
                        <label for="description" class="text-sm font-medium text-gray-900 block mb-1 dark:text-gray-300">Opis produktu</label>
                        <input type="Text" name="productDescrition" id="description" value="" placeholder="Opis" class="bg-gray-50 border border-gray-300 text-gray-900 sm:text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-600 dark:border-gray-500 dark:placeholder-gray-400 dark:text-white" required="">
                    </div>
                    <label for="color" class="text-sm font-medium text-gray-900 block mb-1 dark:text-gray-300">Farba Produktu</label>
                    <input type="Text" name="productColor" id="color" value="" placeholder="White, Pink, Black" class="bg-gray-50 border border-gray-300 text-gray-900 sm:text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-600 dark:border-gray-500 dark:placeholder-gray-400 dark:text-white" required="">

                    <label for="productCount" class="text-sm font-medium text-gray-900 block mb-1 dark:text-gray-300">Počet na sklade</label>
                    <input type="Text" name="productCount" id="productCount" value="" placeholder="Počet na sklade" class="bg-gray-50 border border-gray-300 text-gray-900 sm:text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-600 dark:border-gray-500 dark:placeholder-gray-400 dark:text-white" required="">

                    <label for="productPrice" class="text-sm font-medium text-gray-900 block mb-1 dark:text-gray-300">Cena za kus</label>
                    <input type="Text" name="productPrice" id="productPrice" value="" placeholder="Cena" class="bg-gray-50 border border-gray-300 text-gray-900 sm:text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-600 dark:border-gray-500 dark:placeholder-gray-400 dark:text-white" required="">

                    <label for="productSize" class="text-sm font-medium text-gray-900 block mb-1 dark:text-gray-300">Veľkosť</label>
                    <input type="Text" name="productSize" id="productSize" value="" placeholder="S,M,L,XL,XXL" class="bg-gray-50 border border-gray-300 text-gray-900 sm:text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-600 dark:border-gray-500 dark:placeholder-gray-400 dark:text-white" required="">
                    
                    <label for="protuctType" class="text-sm font-medium text-gray-900 block mb-1 dark:text-gray-300">Typ</label>
                    <input type="Text" name="protuctType" id="protuctType" value="" placeholder="ShortSleeve, LongSleeve,.." class="bg-gray-50 border border-gray-300 text-gray-900 sm:text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-600 dark:border-gray-500 dark:placeholder-gray-400 dark:text-white" required="">

                    <label for="productCategory" class="text-sm font-medium text-gray-900 block mb-1 dark:text-gray-300">Kategoria</label>
                    <input type="Text" name="productCategory" id="productCategory" value="" placeholder="Kids, Man, Women" class="bg-gray-50 border border-gray-300 text-gray-900 sm:text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-600 dark:border-gray-500 dark:placeholder-gray-400 dark:text-white" required="">


                    <button style="background: gray" type="submit" class="text-white rounded-md br w-24 h-12 bg-gray-600 hover:bg-orange-400"> Uložiť</input>
                </form>
            </div>
        </div>
        <div class=" w-[90%] flex justify-center h-60 ml-5 md:ml-0 md:justify-center items-center gap-6 mt-4 md:mt-0">
            <button  class="text-white rounded-md br w-24 h-12 bg-gray-600 hover:bg-orange-400"><a href="/">Domov</a></button>

            <button id="dropdownDefaultButtonCreate" data-dropdown-toggle="dropdownCreate" data-trigger="click"
 class="text-white rounded-md br w-24 h-12 bg-gray-600 hover:bg-orange-400">Vytvoriť</button>
        </div>
    </section>
@endsection