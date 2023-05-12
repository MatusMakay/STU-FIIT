<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\SoftDeletes;

class product extends Model
{
    use HasFactory;
    // creating of deleted att in the table so we can have the history of deleted events
    use SoftDeletes;
    protected $dates = ['deleted_at'];

    // creating the relation between the tables in the database
    public function admin(){
        return $this->belongsTo('app\admin');
    }

    public function shopping_cart(){
        return $this->belongsToMany('app\product', 'product_cart', 'product_id', 'user_id');
    }

    public function categories(){
        return $this->hasMany('app\categories');
    }

}
