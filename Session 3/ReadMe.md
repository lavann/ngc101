In this session we will create the UI that will talk to the API, that we created in an earlier session (session 2)

From the command line, use the ng cli to create our project with the following command

ng new books-ui
![cli](./images/cli.png)

NG cli will request user prompts for the following:
![angular-routing](./images/angularRouting.png)

Y - Angular Routing
![angular-routing](./images/yesRouting.png)

Select CSS for style sheet
![CSS](./images/css.png)

cli will install all relevant dependencies
![depend](./images/dependancy.png)

Navigate to the folder that has been created by the cli
![nav](./images/navConsole.png)

Launch VS Code - Code .
![launchCode](./images/launchCode.png)

Invoke the CLI to serve the application
![ng1](./images/ngServe1.png)

![ng2](./images/ngServe2.png)

Launch the browser and navigate to http://localhost:4200, you will be presented with the application that the CLI has created.
![browser](./images/browser.png)

Within VS Code, locate app.component.html
SRC => APP => app.component.html
![VS Code App Component](./images/vscode_app_component1.png)

Select all of the content within the file CTRL + A, and then delete.
![VS Code App Component](./images/vscode_app_component2.png)

You should be faced with an empty app.component.html
![VS Code App Component](./images/vscode_app_component3.png)

We are now going to add the Bootstrap CSS framework, to add styling as well as make our application responsive.

From your browser, navigate to
https://getbootstrap.com/docs/4.0/getting-started/introduction/#css

![bootstrap](./images/bootstrap1.png)

Navigate to Index to HTML and add the following entry to reference the bootstrap CSS.

```
<!doctype html>
<html lang="en">

<head>
  <meta charset="utf-8">
  <title>BooksUi</title>
  <base href="/">
  <meta name="viewport" content="width=device-width, initial-scale=1">
  <link rel="icon" type="image/x-icon" href="favicon.ico">
  <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css"
    integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm" crossorigin="anonymous">
</head>

<body>
  <app-root></app-root>
</body>

</html>
```

Once you have saved the file after editing, the CLI will recompile the application. The browser will refresh - at this point it will be a blank.

We will now begin to add content to our application, using a mixture of Bootstrap styles and Angular Components.

Angular applications are built using a few simple building blocks : [Modules](https://angular.io/guide/architecture#modules) and [Components](https://angular.io/guide/architecture#components).

We will now create our first component, our "Home" Component", this will be the first page that is loaded when a user navigates to our application.

From the command line enter [NG G C](https://angular.io/cli#command-overview) home - this is short hand for NG Generate Component home (home being the name of our component).

```
ng g c home
```

Navigate to update the view, navigate to home.component.html
We are going to leverage the [Jumbotron](https://getbootstrap.com/docs/4.0/components/jumbotron/) Bootstrap Component to make a call out

```
<div class="row">
    <div class="col-12">
        <div class="jumbotron">
            <h1 class="display-4">Authors Management System</h1>
            <p class="lead">This is a simple hero unit, a simple jumbotron-style component for calling extra attention
                to
                featured content or information.</p>
            <hr class="my-4">
            <p>It uses utility classes for typography and spacing to space content out within the larger container.</p>
            <p class="lead">
                <a class="btn btn-primary btn-lg" href="#" role="button">Learn more</a>
            </p>
        </div>
    </div>
</div>

```

The Jumbotron is encapsulated with two bootstrap Styles, [row and col-12](https://getbootstrap.com/docs/4.0/layout/grid/#how-it-works), this component is further encapsulated in a [container](https://getbootstrap.com/docs/4.0/layout/overview/#containers) Bootstrap Style; this will be added to app.component.html shortly. This allows for our application to behave in a responsive manner.

To view our newly created component, we can leverage the [ css selector](https://angular.io/guide/architecture-components) value from home.component.ts as a html element

```
import { Component } from '@angular/core';

@Component({
  selector: 'app-root', (This is used as a html element in our html)
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'books-ui';


}
```

In app.component.html - enter

```
<app-home></app-home>
```

Once recompiled, you will be presented with our jumbotron
![callout](./images/jumbotron1.png)

We've now created our component and used it like a html element i.e
` <div> </div>` with Angular taking care of creating an inserting an instance of home.component whenever it finds ` <app-home> </app-home>`.
