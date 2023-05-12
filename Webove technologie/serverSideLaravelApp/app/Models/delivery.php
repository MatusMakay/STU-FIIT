<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\SoftDeletes;

class delivery extends Model
{
    use HasFactory;
    // creating of deleted att in the table so we can have the history of deleted events
    use SoftDeletes;
    protected $dates = ['deleted_at'];

}
