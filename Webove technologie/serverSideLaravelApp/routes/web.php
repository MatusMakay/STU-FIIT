<?php

use App\Http\Controllers\AdminController;
use App\Http\Controllers\ProfileController;
use Illuminate\Support\Facades\Route;
use App\Http\Controllers\ProductController;
use App\Http\Controllers\ShoppingCartController;
use App\Http\Controllers\PaymentController;
use App\Http\Controllers\OrderController;
use Illuminate\Http\Request;
use App\Http\Controllers\Controller;
/*
|--------------------------------------------------------------------------
| Web Routes
|--------------------------------------------------------------------------
|
| Here is where you can register web routes for your application. These
| routes are loaded by the RouteServiceProvider and all of them will
| be assigned to the "web" middleware group. Make something great!
|
*/

Route::get('/admin', [AdminController::class, 'index']);

Route::put('/admin/{id}', [AdminController::class, 'update']);
Route::delete('/admin/{id}', [AdminController::class, 'destroy']);
Route::post('/admin', [AdminController::class, 'store']);


Route::get('order/summary', [OrderController::class, 'GETsummary']);
Route::get('/sort', [ProductController::class, 'sort']);
Route::get('/filter', [ProductController::class, 'filter']);

Route::get('/', [Controller::class, 'GEThome']);

//[ProductController::class, 'addItemToCart']
Route::post('/products/{product}', [ProductController::class, 'addItemToCart'])->name('add-to-cart');

Route::resource('/products', ProductController::class);

//Route::get('/cart', [ShoppingCartController::class, 'addToCart'])->name('cart');
//Route::get('/add', [ShoppingCartController::class, 'addToCart']);

Route::post('/add', [ShoppingCartController::class, 'addToCart']);

Route::resource('cart', ShoppingCartController::class);

Route::post('cart', [ShoppingCartController::class, 'addToCart'])->name('cart');
Route::get('/cart', [ShoppingCartController::class, 'index']);
Route::post('/cart/{id}', [ShoppingCartController::class, 'delete'])->name('remove-from-cart');

Route::get('/search', [ProductController::class, 'search'])->name('search');

Route::get('/payment', [PaymentController::class, 'GETpayment']);

Route::get('/payment/delivery', [PaymentController::class, 'GETdelivery']);

Route::get('order/summary', [OrderController::class, 'GETsummary']);




Route::get('/dashboard', function () {
    return view('dashboard');
})->middleware('auth')->name('dashboard');

Route::middleware('auth')->group(function () {
    Route::get('/profile', [ProfileController::class, 'edit'])->name('profile.edit');
    Route::patch('/profile', [ProfileController::class, 'update'])->name('profile.update');
    Route::delete('/profile', [ProfileController::class, 'destroy'])->name('profile.destroy');
});

require __DIR__.'/auth.php';
