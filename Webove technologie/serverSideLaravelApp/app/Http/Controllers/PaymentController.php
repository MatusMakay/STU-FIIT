<?php

namespace App\Http\Controllers;

use App\Http\Requests\StorepaymentRequest;
use App\Http\Requests\UpdatepaymentRequest;
use App\Models\payment;
use Illuminate\Http\Request;
use Illuminate\Validation\ValidationException;
use App\Models\product;


class PaymentController extends Controller
{
    /**
     * Display a listing of the resource.
     */
    public function index()
    {
        //
    }

    public function GETpayment(){
        return view('layouts.payment.info');
    }

    public function GETdelivery(){
        return view('layouts.payment.delivery');
    }

    public function GETpaymentDetail(){
        return view('layouts.payment.payment-details');
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
    public function store(StorepaymentRequest $request)
    {
        //
    }

    /**
     * Display the specified resource.
     */
    public function show(payment $payment)
    {
        //
    }

    /**
     * Show the form for editing the specified resource.
     */
    public function edit(payment $payment)
    {
        //
    }

    /**
     * Update the specified resource in storage.
     */
    public function update(UpdatepaymentRequest $request, payment $payment)
    {
        //
    }

    /**
     * Remove the specified resource from storage.
     */
    public function destroy(payment $payment)
    {
        //
    }

    public function checkPayment(Request $request){
        if(!($request->has('out') || $request->has('home')) && !($request->has('card') || $request->has('money'))){
            throw ValidationException::withMessages(['please enter the delivery or payment method']);
        }
        else if ($request->has('out') && $request->has('home') && $request->has('card') && $request->has('money')){
            throw ValidationException::withMessages(['Can only choose one from both ']);
        }
        else if (($request->has('out') && $request->has('home')) || ($request->has('card') && $request->has('money'))){
            throw ValidationException::withMessages(['Can only choose one from both ']);
        }

        if (!session()->has('payment_method')){
            session()-> put('payment_method');
        }
        if (!session()->has('delivery_method')){
            session()-> put('delivery_method');
        }

        session()->push('delivery_method',  $request->has('out') ? 'out' : 'home');
        session()->push('payment_method', $request->has('card') ? 'card' : 'money');

        return  view('layouts.payment.delivery');
    }

    public function checkInfo(Request $request){
       if(!$request->email || !$request->address || !$request->psc || !$request->city){
             return view('layouts.payment.delivery', ['error' => 'Please fill out the whole form']);
       }
       $validate  = explode('@', $request->email)[1]??null;
       if (!$validate){
        return view('layouts.payment.delivery', ['error' => 'Wrong email format']);
       }

       $totalPrice = 0;
       $products = collect();
       $product_ids = session()->get('cart');
       $delivery_method = session()->get('delivery_method');
       if($product_ids == null){
           return view('layouts.cart.cart', ["products" => null, 'count' => null]);
       }

       foreach($product_ids as $product_id){
           $product = product::where('id', $product_id)->first();
           $totalPrice += $product->product_price;
           $products->push($product);
       }

        session()-> flush();

        return view('layouts.summary.summary', ['products' => $products, 'userAdress' => $request->address, 'userEmail' => $request->email, 'deliveryMethod' => $delivery_method[0], 'totalPrice' => $totalPrice]);
    }
}
