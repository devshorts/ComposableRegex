ComposableRegex
===============

This is a demo of building composable regular expressions. This code is hosted on http://composableregex.apphb.com for demo.  For more info read https://devshorts.github.io/onoffswitch/2013/05/06/composable-regex/

For example, we can now do this

```
weirdChars = (!|-|\+|\\|\$|\^|~|#|%|\?|{|}|_|/|=)
numbers = \d
characters = [A-z]
anyChars = (weirdChars|numbers|characters)
             
lettersFollowedBySingleDot = (anyChars+\.anyChars+)
             
names = anyChars|lettersFollowedBySingleDot
             
onlyQuotableCharacters = @|\s
quotedNames = ""(names|onlyQuotableCharacters)+""
 
anyValidStart = (names|quotedNames)+
 
group = (quotedNames:anyValidStart)|anyValidStart
 
local = ^(group)
 
ipv4 = ((\d{1,3}.){3}(\d{1,3}))
 
ipv6Entry = ([a-f]|[A-F]|[0-9]){4}? ## group of 4 hex values
ipv6 = ((ipv6Entry:){7}?ipv6Entry) ## 8 groups of ipv6 entries
 
comAddresses = (characters+(\.characters+)*) ## stuff like a.b.c.d etc
domain = (comAddresses|ipv6|ipv4)$ ## this has to be at the end
 
(local)@(domain)
```

Which compiles to:

```
(^(("(((!|-|\+|\\|\$|\^|~|#|%|\?|{|}|_|/|=)|\d|[A-z])|(((!|-|\+|\\|\$|\^|~|#|%
|\?|{|}|_|/|=)|\d|[A-z])+\.((!|-|\+|\\|\$|\^|~|#|%|\?|{|}|_|/|=)|\d|[A-z])+)|@|
\s)+":(((!|-|\+|\\|\$|\^|~|#|%|\?|{|}|_|/|=)|\d|[A-z])|(((!|-|\+|\\|\$|\^|~|#|%
|\?|{|}|_|/|=)|\d|[A-z])+\.((!|-|\+|\\|\$|\^|~|#|%|\?|{|}|_|/|=)|\d|[A-z])+)|"((
(!|-|\+|\\|\$|\^|~|#|%|\?|{|}|_|/|=)|\d|[A-z])|(((!|-|\+|\\|\$|\^|~|#|%|\?|{|}|_
|/|=)|\d|[A-z])+\.((!|-|\+|\\|\$|\^|~|#|%|\?|{|}|_|/|=)|\d|[A-z])+)|@|\s)+")+)|(
((!|-|\+|\\|\$|\^|~|#|%|\?|{|}|_|/|=)|\d|[A-z])|(((!|-|\+|\\|\$|\^|~|#|%|\?|{|}|
_|/|=)|\d|[A-z])+\.((!|-|\+|\\|\$|\^|~|#|%|\?|{|}|_|/|=)|\d|[A-z])+)|"(((!|-|\+|
\\|\$|\^|~|#|%|\?|{|}|_|/|=)|\d|[A-z])|(((!|-|\+|\\|\$|\^|~|#|%|\?|{|}|_|/|=)|\d
|[A-z])+\.((!|-|\+|\\|\$|\^|~|#|%|\?|{|}|_|/|=)|\d|[A-z])+)|@|\s)+")+))@((([A-z]+
(\.[A-z]+)*)|((([a-f]|[A-F]|[0-9]){4}?:){7}?([a-f]|[A-F]|[0-9]){4}?)|((\d{1,3}.)
{3}(\d{1,3})))$)
```
