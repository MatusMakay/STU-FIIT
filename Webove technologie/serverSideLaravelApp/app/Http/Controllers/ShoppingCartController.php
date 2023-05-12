<?php

namespace App\Http\Controllers;

use App\Http\Requests\Updateshopping_cartRequest;
use App\Models\product;
use App\Models\shopping_cart;
use Carbon\PHPStan\LazyMacro;
use Illuminate\Http\JsonResponse;
use Illuminate\Http\Request;
use Illuminate\Http\RedirectResponse;
use Illuminate\Http\Resources\Json\JsonResource;

class ShoppingCartController extends Controller
{

    /**
     * Display a listing of the resource.
     */
    public function index()
    {
        $count = collect();
        $products = collect();
        if(!session()->has('cart')){
            return view('layouts.cart.cart', ["products" => null, 'count' => null]);
        }
        $product_ids = session()->get('cart');
        if($product_ids === null){
            return view('layouts.cart.cart', ["products" => null, 'count' => null]);
        }

        foreach($product_ids as $product_id){
            $product = product::where('id', $product_id)->first();
            $products->push($product);
        }

        return view('layouts.cart.cart', ["products" => $products, 'count' => $count]);
    }

    /**
     * Show the form for creating a new resource.
     */
    public function create()
    {
        //
    }

    /**
     * Store a newly created resource in storage.
     */

    /**
     * Display the specified resource.
     */
    public function show(shopping_cart $shopping_cart)
    {
        $count = collect();
        $products = collect();
        $product_ids = session()->get('cart');
        foreach($product_ids as $product_id){
            $product = product::where('id', $product_id)->first();
            $products->push($product);
        }
            return view('layouts.cart.cart', ["products" => $products, 'count' => $count]);

    }

    /**
     * Show the form for editing the specified resource.
     */
    public function edit(shopping_cart $shopping_cart)
    {
        //
    }

    /**
     * Update the specified resource in storage.
     */
    public function update(Updateshopping_cartRequest $request, shopping_cart $shopping_cart)
    {
        //
    }

    /**
     * Remove the specified resource from storage.
     */
    public function destroy(shopping_cart $shopping_cart)
    {
        dd("ouno");
    }
    public function delete($id){
        dd("ouno");
        if (!session()->has('cart')){
            return response('id', 400, ['id' => $id]);
        }
        $cart = session()->get('cart');
        session()->forget('cart');
        $cart = array_diff_key($cart, array($id));
        session()->put('cart', $cart);
        return new JsonResponse(['id' => $id], );
    }


    public function checkContinue (Request $request){
        if(!session()->has('cart')){
        return view('layouts.cart.cart', ["products" => null, 'count' => null]);
        }
        return  view('layouts.payment.info');
    }
}

