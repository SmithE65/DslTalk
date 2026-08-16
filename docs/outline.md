# DSL Talk

## Introduction

### About Me

> Introduce yourself.

### Why this talk?

- Talk to Crafting Interpreters book
- Talk to SchemaShift
- Emphasize how straightforward it turned out to be; this is not CS PhD territory

### What is a DSL?

- Domain Specific Language
- Define DSL and contrast to GPL (general purpose language)
- Talk to common DSLs SQL, cron, RegEx, YAML, HTML, ask about INI, Mermaid

A basic definition:
- Focused vocabulary
- Focused domain
- Restricted semantics
- Not necessarily textual
- Not necessarily Turing complete

### A live example: A Calculator

```
2 + 3
2 + 3 * 4
(2 + 3) * 4
10 / 2 - 1
-5 + 10
```

### Scanning and Tokens

- Provide the sample string (1 + 2) * 3 (enable tool tips for each token)
- Ask "how do we parse this?" and "what are the smallest meaningful bits of information we can extract?"
- Introduce "token".  We've all heard the term thanks to AI but what do we mean here?
- Give "number", "left paren", "right paren", "plus", "multiply"

### A Side About Grammar

- Use the `(1 + 2) * 3` example to introduce language grammar
- What's legal?
- What's illegal?
- The goal is unambiguous syntax.
- Talk to precedence

```
expression  → term (("+" | "-") term)*
term        → factor (("*" | "/") factor)*
factor      → NUMBER | "(" expression ")"
```

### The Abstract Syntax Tree (AST)

- How language syntax is expressed in code
- May not align with textual syntax, ex. parenthetical grouping may not need AST representation.
- How is `(1 + 2) * 3` represented?

### The Recursive Descent Parser
