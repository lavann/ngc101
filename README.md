# MyDreamApp

This project was generated with [Angular CLI](https://github.com/angular/angular-cli) version 10.0.4.

## Development server

Run `ng serve` for a dev server. Navigate to `http://localhost:4200/`. The app will automatically reload if you change any of the source files.

## Code scaffolding

Run `ng generate component component-name` to generate a new component. You can also use `ng generate directive|pipe|service|class|guard|interface|enum|module`.

## Build

Run `ng build` to build the project. The build artifacts will be stored in the `dist/` directory. Use the `--prod` flag for a production build.

## Running unit tests

Run `ng test` to execute the unit tests via [Karma](https://karma-runner.github.io).

## Running end-to-end tests

Run `ng e2e` to execute the end-to-end tests via [Protractor](http://www.protractortest.org/).

## Further help

To get more help on the Angular CLI use `ng help` or go check out the [Angular CLI README](https://github.com/angular/angular-cli/blob/master/README.md).


## For CSS Styling we use the Bootstrap (4) CSS Framework

Bootstrap uses "Containers", "Rows", and "Grids" for layout and alignment of content in a web application.

* the container CSS style, are used to establish the width for content layout. They are applied to the  `<div>` element i.e

```html 
    <div class="container"> </div>
````

* within containers, you can place rows; rows display the width of the container
```html
 <div class="container">
    <div class="row"> 

    </div>
</div>
```

* within rows, you can place columns. There can be up to 12 columns per row. If you wanted to have 2 equal width columns you apply the following css style .col-4.  Columns can be responsive, using the responsive breakpoints
```html
 <div class="container">
    <div class="row"> 
        <div class="col-6">
            Content goes here
        </div>
        <div class=col-6>
            Content goes here
        </div>
    </div>
</div>
```

* For further information, around the [Bootstrap Grid System](https://getbootstrap.com/docs/4.0/layout/grid/#grid-options)

* We used the following components
    * [Navigational element](https://getbootstrap.com/docs/4.0/components/navs/#base-nav)
    * [Jumbotron](https://getbootstrap.com/docs/4.0/components/jumbotron/)

* During the session that we cover to build the UI, we will leverage the following components and styling when we build out the UI.
  * [Tables](https://getbootstrap.com/docs/4.0/content/tables/)
  * [Buttons](https://getbootstrap.com/docs/4.0/components/buttons/)
  * [Forms](https://getbootstrap.com/docs/4.0/components/forms/)
  * [Cards](https://getbootstrap.com/docs/4.0/components/card/)